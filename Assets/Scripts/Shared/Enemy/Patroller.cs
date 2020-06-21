using Assets.Scripts.Constants;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(KillableEnemy))]
    [RequireComponent(typeof(PositionableEntity))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Patroller : MonoBehaviour
    {
        public Tilemap CheckpointsTilemap;

        #region Properties
        private Animator animator;
        private List<Vector2> checkpointPositions;
        private Queue<CheckpointDirection> currentPatrollingDirections;
        private bool isPatrolling;
        private KillableEnemy killableEnemyActor;
        private Vector2 nextPatrollingPosition;
        private PositionableEntity positionableEntity;
        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;
        #endregion

        void Awake()
        {
            InitializeProperties();
            FocusClosestCheckpoint();
        }

        void Update()
        {
            if (isPatrolling)
            {
                if (killableEnemyActor.IsDead())
                {
                    StopPatrolling();
                    return;
                }

                if (IsInNextPatrollingPosition())
                    FetchFollowingPatrollingPosition();

                PatrollToNextPosition();
            }
        }

        public void StopPatrolling()
        {
            if (!isPatrolling)
                return;

            animator.SetBool(EnemyAnimatorConstants.IsWalkingParameter, false);
            rigidbody2D.velocity = Vector2.zero;
            spriteRenderer.flipX = false;
            isPatrolling = false;
        }

        protected abstract Queue<CheckpointDirection> GetPatrollingDirections();

        protected abstract float GetPatrollingSpeed();

        #region Helpers
        private void FetchFollowingPatrollingPosition()
        {
            if (currentPatrollingDirections.Count == 0)
                currentPatrollingDirections = GetPatrollingDirections();

            var nextPatrollingDirection = currentPatrollingDirections.Dequeue();

            nextPatrollingPosition = CheckpointDirectionStrategy.GetStrategies()
                .First(strategy => strategy.IsApplicable(nextPatrollingDirection))
                .GetClosestCheckpointPosition(nextPatrollingPosition, checkpointPositions)
                .Value;
        }

        private void FocusClosestCheckpoint()
        {
            nextPatrollingPosition = checkpointPositions
                .Select(position => new
                {
                    Position = position,
                    DistanceToPosition = Vector3.Distance(transform.position, position)
                })
                .OrderBy(positionInfo => positionInfo.DistanceToPosition)
                .First()
                .Position;
        }

        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
            currentPatrollingDirections = GetPatrollingDirections();
            isPatrolling = true;
            killableEnemyActor = GetComponent<KillableEnemy>();
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

        private bool IsInNextPatrollingPosition() => nextPatrollingPosition == positionableEntity.GetPosition();

        private void PatrollToNextPosition()
        {
            const float CheckpointPositionThreshold = 0.05f;

            if (Vector2.Distance(positionableEntity.GetPosition(), nextPatrollingPosition) <= CheckpointPositionThreshold)
            {
                positionableEntity.SetPosition(nextPatrollingPosition);
                return;
            }

            var velocity = (nextPatrollingPosition - positionableEntity.GetPosition()).normalized * GetPatrollingSpeed();

            rigidbody2D.velocity = velocity;
            spriteRenderer.flipX = velocity.x < -0.1f;
            animator.SetBool(HeroAnimatorConstants.IsWalkingParameter, true);
        }
        #endregion
    }
}
