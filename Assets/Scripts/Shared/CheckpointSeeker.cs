using Assets.Scripts.Constants;
using Assets.Scripts.Shared.SeekerDirectionStrategies;
using Asyncoroutine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Shared
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CheckpointSeeker : MonoBehaviour
    {
        public Tilemap CheckpointsTilemap;

        #region Properties
        private readonly Vector2 PositionOffset = new Vector2(0.5f, 1.5f);
        private const float Speed = 2.5f;

        private Animator animator;
        private List<Vector2> checkpointPositions;
        private new Rigidbody2D rigidbody2D;
        private List<ISeekerDirectionStrategy> seekerDirectionStrategies;
        private Vector2 seekingCheckpointPosition;
        private SpriteRenderer spriteRenderer;
        #endregion

        void Start()
        {
            animator = GetComponent<Animator>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            InitializeSeekerDirectionStrategies();
            InitializeCheckpointPositions();
            FocusClosestCheckpoint();
        }

        void Update()
        {
            if (IsInCheckpointPosition())
                Idle();
            else
                SeekCheckpointPosition();
        }

        public async Task BeginSeekingCheckpointAsync(SeekerDirection direction)
        {
            var oldSeekingCheckpointPosition = seekingCheckpointPosition;
            var newSeekingCheckpointPosition = seekerDirectionStrategies
                .First(strategy => strategy.IsApplicable(direction))
                .GetClosestCheckpointPosition(seekingCheckpointPosition, checkpointPositions);

            if (!newSeekingCheckpointPosition.HasValue)
                return;

            seekingCheckpointPosition = newSeekingCheckpointPosition.Value;

            var distanceToCheckpoint = Vector2.Distance(GetCurrentPosition(), newSeekingCheckpointPosition.Value);
            var aproxTimeToReachCheckpoint = Mathf.CeilToInt(distanceToCheckpoint * 2 / Speed);
            var seekingStartingTime = Time.time;

            await new WaitUntil(() => IsInCheckpointPosition() || HasTakenTooLongToReachCheckpoint());

            if (!IsInCheckpointPosition())
            {
                seekingCheckpointPosition = oldSeekingCheckpointPosition;

                await new WaitUntil(() => IsInCheckpointPosition());
            }

            bool HasTakenTooLongToReachCheckpoint() => Time.time - seekingStartingTime >= aproxTimeToReachCheckpoint;
        }

        public Vector2 GetCurrentPosition() => new Vector2(transform.position.x, transform.position.y) - PositionOffset;

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

            transform.position = seekingCheckpointPosition + PositionOffset;
        }

        private void Idle()
        {
            animator.SetBool(HeroAnimatorConstants.IsWalkingParameter, false);
            rigidbody2D.velocity = Vector2.zero;
            spriteRenderer.flipX = false;
        }

        private void InitializeCheckpointPositions()
        {
            checkpointPositions = new List<Vector2>();

            if (CheckpointsTilemap == null)
                return;

            foreach (var position in CheckpointsTilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(position.x, position.y, position.z);
                var place = CheckpointsTilemap.CellToWorld(localPlace);

                if (CheckpointsTilemap.HasTile(localPlace))
                    checkpointPositions.Add(place);
            }
        }

        private void InitializeSeekerDirectionStrategies()
        {
            seekerDirectionStrategies = new List<ISeekerDirectionStrategy>
            {
                new UpSeekerDirectionStrategy(),
                new DownSeekerDirectionStrategy(),
                new LeftSeekerDirectionStrategy(),
                new RightSeekerDirectionStrategy()
            };
        }

        private bool IsInCheckpointPosition() => GetCurrentPosition() == seekingCheckpointPosition;

        private void SeekCheckpointPosition()
        {
            const float checkpointPositionThreshold = 0.05f;

            if (Vector2.Distance(GetCurrentPosition(), seekingCheckpointPosition) <= checkpointPositionThreshold)
            {
                transform.position = seekingCheckpointPosition + PositionOffset;
                return;
            }

            var velocity = (seekingCheckpointPosition - GetCurrentPosition()).normalized * Speed;

            rigidbody2D.velocity = velocity;
            spriteRenderer.flipX = velocity.x < 0;
            animator.SetBool(HeroAnimatorConstants.IsWalkingParameter, true);
        }
        #endregion
    }
}
