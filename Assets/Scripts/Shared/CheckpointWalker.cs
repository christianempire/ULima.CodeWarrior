using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Shared
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(KillableEntity))]
    [RequireComponent(typeof(PositionableEntity))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class CheckpointWalker : MonoBehaviour
    {
        public Tilemap CheckpointsTilemap;

        #region Properties
        private Animator animator;
        private List<Vector2> checkpointPositions;
        private bool isWalking;
        private KillableEntity killableEntity;
        private Vector2? nextWalkingPosition;
        private PositionableEntity positionableEntity;
        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;
        private Queue<CheckpointDirection> walkingDirections;
        #endregion

        void Awake()
        {
            InitializeProperties();
            FocusClosestCheckpoint();
        }

        void Update()
        {
            if (isWalking)
            {
                if (killableEntity.IsDead())
                {
                    StopWalking();
                    return;
                }

                if (IsInNextWalkingPosition())
                {
                    FetchFollowingWalkingPosition();

                    if (nextWalkingPosition == null)
                    {
                        StopWalking();
                        return;
                    }
                }

                WalkToNextPosition();
            }
        }

        protected abstract string GetAnimatorIsWalkingParameter();

        protected abstract Queue<CheckpointDirection> GetWalkingDirections();

        protected abstract float GetWalkingSpeed();

        #region Helpers
        private void FetchFollowingWalkingPosition()
        {
            if (walkingDirections.Count == 0)
            {
                nextWalkingPosition = null;
                return;
            }

            var nextWalkingDirection = walkingDirections.Dequeue();

            nextWalkingPosition = CheckpointDirectionStrategy.GetStrategies()
                .First(strategy => strategy.IsApplicable(nextWalkingDirection))
                .GetClosestCheckpointPosition(nextWalkingPosition.Value, checkpointPositions)
                .Value;
        }

        private void FocusClosestCheckpoint()
        {
            nextWalkingPosition = checkpointPositions
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
            isWalking = true;
            killableEntity = GetComponent<KillableEntity>();
            positionableEntity = GetComponent<PositionableEntity>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            walkingDirections = GetWalkingDirections();

            checkpointPositions = new List<Vector2>();
            foreach (var position in CheckpointsTilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(position.x, position.y, position.z);
                var place = CheckpointsTilemap.CellToWorld(localPlace);

                if (CheckpointsTilemap.HasTile(localPlace))
                    checkpointPositions.Add(place);
            }
        }

        private bool IsInNextWalkingPosition() => nextWalkingPosition.Value == positionableEntity.GetPosition();

        public void StopWalking()
        {
            if (!isWalking)
                return;

            animator.SetBool(GetAnimatorIsWalkingParameter(), false);
            rigidbody2D.velocity = Vector2.zero;
            spriteRenderer.flipX = false;
            isWalking = false;
        }

        private void WalkToNextPosition()
        {
            const float CheckpointPositionThreshold = 0.05f;

            if (Vector2.Distance(positionableEntity.GetPosition(), nextWalkingPosition.Value) <= CheckpointPositionThreshold)
            {
                positionableEntity.SetPosition(nextWalkingPosition.Value);
                return;
            }

            var velocity = (nextWalkingPosition.Value - positionableEntity.GetPosition()).normalized * GetWalkingSpeed();

            rigidbody2D.velocity = velocity;
            spriteRenderer.flipX = velocity.x < -0.1f;
            animator.SetBool(GetAnimatorIsWalkingParameter(), true);
        }
        #endregion
    }
}
