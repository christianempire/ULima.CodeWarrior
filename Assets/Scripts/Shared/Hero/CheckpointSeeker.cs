using Assets.Scripts.Constants.Hero;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using Asyncoroutine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Shared.Hero
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(KillableHero))]
    [RequireComponent(typeof(PositionableEntity))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CheckpointSeeker : MonoBehaviour
    {
        public Tilemap CheckpointsTilemap;

        #region Properties
        private const float Speed = 2.5f;

        private Animator animator;
        private List<Vector2> checkpointPositions;
        private KillableHero killableHero;
        private bool mustSeekCheckpointPosition;
        private PositionableEntity positionableEntity;
        private new Rigidbody2D rigidbody2D;
        private Vector2 seekingCheckpointPosition;
        private SpriteRenderer spriteRenderer;
        #endregion

        void Awake()
        {
            InitializeProperties();
            FocusClosestCheckpoint();
        }

        void Update()
        {
            if (mustSeekCheckpointPosition)
                SeekCheckpoint();
        }

        public async Task SeekCheckpointAsync(CheckpointDirection direction)
        {
            var oldSeekingCheckpointPosition = seekingCheckpointPosition;
            var newSeekingCheckpointPosition = CheckpointDirectionStrategy.GetStrategies()
                .First(strategy => strategy.IsApplicable(direction))
                .GetClosestCheckpointPosition(seekingCheckpointPosition, checkpointPositions);

            if (!newSeekingCheckpointPosition.HasValue)
                return;

            mustSeekCheckpointPosition = true;
            seekingCheckpointPosition = newSeekingCheckpointPosition.Value;

            var distanceToCheckpoint = Vector2.Distance(positionableEntity.GetPosition(), newSeekingCheckpointPosition.Value);
            var aproxTimeToReachCheckpoint = Mathf.CeilToInt(distanceToCheckpoint * 2 / Speed);
            var seekingStartingTime = Time.time;

            await new WaitUntil(() => IsInCheckpointPosition() || HasTakenTooLongToReachCheckpoint() || killableHero.IsDead());

            if (!IsInCheckpointPosition() && !killableHero.IsDead())
            {
                seekingCheckpointPosition = oldSeekingCheckpointPosition;

                await new WaitUntil(() => IsInCheckpointPosition() || killableHero.IsDead());
            }

            StopSeekingCheckpoint();

            mustSeekCheckpointPosition = false;

            bool HasTakenTooLongToReachCheckpoint() => Time.time - seekingStartingTime >= aproxTimeToReachCheckpoint;
        }

        #region Helpers
        private void FocusClosestCheckpoint()
        {
            seekingCheckpointPosition = checkpointPositions
                .Select(position => new
                {
                    Position = position,
                    DistanceToPosition = Vector3.Distance(transform.position, position)
                })
                .OrderBy(positionInfo => positionInfo.DistanceToPosition)
                .First()
                .Position;

            positionableEntity.SetPosition(seekingCheckpointPosition);
        }

        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
            killableHero = GetComponent<KillableHero>();
            mustSeekCheckpointPosition = false;
            positionableEntity = GetComponent<PositionableEntity>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            checkpointPositions = new List<Vector2>();
            foreach (var position in CheckpointsTilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(position.x, position.y, position.z);
                var place = CheckpointsTilemap.CellToWorld(localPlace);

                if (CheckpointsTilemap.HasTile(localPlace))
                    checkpointPositions.Add(place);
            }
        }

        private bool IsInCheckpointPosition() => positionableEntity.GetPosition() == seekingCheckpointPosition;

        private void SeekCheckpoint()
        {
            const float CheckpointPositionThreshold = 0.05f;

            if (Vector2.Distance(positionableEntity.GetPosition(), seekingCheckpointPosition) <= CheckpointPositionThreshold)
            {
                positionableEntity.SetPosition(seekingCheckpointPosition);
                return;
            }

            var velocity = (seekingCheckpointPosition - positionableEntity.GetPosition()).normalized * Speed;

            rigidbody2D.velocity = velocity;
            spriteRenderer.flipX = velocity.x < -0.1f;
            animator.SetBool(HeroAnimatorConstants.IsWalkingParameter, true);
        }

        private void StopSeekingCheckpoint()
        {
            animator.SetBool(HeroAnimatorConstants.IsWalkingParameter, false);
            rigidbody2D.velocity = Vector2.zero;
            spriteRenderer.flipX = false;
        }
        #endregion
    }
}
