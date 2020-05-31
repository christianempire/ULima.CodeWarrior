﻿using Assets.Scripts.SharedLevelScripts.SeekerDirectionStrategies;
using Asyncoroutine;
using RPGM.Gameplay;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

namespace Assets.Scripts.SharedLevelScripts
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CheckpointSeeker : MonoBehaviour
    {
        public float Speed = 1.5f;
        public Tilemap CheckpointsTilemap;
        public ConversationScript UnexistentCheckpointConversation;
        public ConversationScript UnreachableCheckpointConversation;

        #region Properties
        private readonly Vector2 PositionOffset = new Vector2(0.5f, 0.25f);

        private Animator animator;
        private List<Vector2> checkpointPositions;
        private PixelPerfectCamera pixelPerfectCamera;
        private new Rigidbody2D rigidbody2D;
        private List<ISeekerDirectionStrategy> seekerDirectionStrategies;
        private Vector2 seekingCheckpointPosition;
        private SpriteRenderer spriteRenderer;
        #endregion

        void Awake()
        {
            animator = GetComponent<Animator>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            pixelPerfectCamera = FindObjectOfType<PixelPerfectCamera>();

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

        void LateUpdate()
        {
            if (pixelPerfectCamera != null)
                transform.position = pixelPerfectCamera.RoundToPixel(transform.position);
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
            animator.SetInteger("WalkX", 0);
            animator.SetInteger("WalkY", 0);
            rigidbody2D.velocity = Vector2.zero;
            spriteRenderer.flipX = true;
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

            animator.SetInteger("WalkX", velocity.x < -0.25f ? -1 : velocity.x > 0.25f ? 1 : 0);
            animator.SetInteger("WalkY", velocity.y < -0.25f ? 1 : velocity.y > 0.25f ? -1 : 0);
            rigidbody2D.velocity = velocity;
            spriteRenderer.flipX = velocity.x >= 0;
        }
        #endregion
    }
}