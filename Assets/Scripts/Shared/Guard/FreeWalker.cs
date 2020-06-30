using Assets.Scripts.Constants.Guard;
using Asyncoroutine;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Guard
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PositionableEntity))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class FreeWalker : MonoBehaviour
    {
        #region Properties
        private Animator animator;
        private Vector2 currentTarget;
        private bool mustWalk;
        private PositionableEntity positionableEntity;
        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        private void Update()
        {
            if (mustWalk)
                Walk();
        }

        public async Task WalkAsync(Vector2 relativeTarget)
        {
            mustWalk = true;
            currentTarget = positionableEntity.GetPosition() + relativeTarget;

            await new WaitUntil(() => IsInTargetPosition());

            mustWalk = false;

            StopWalking();
        }

        #region Helpers
        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
            mustWalk = false;
            positionableEntity = GetComponent<PositionableEntity>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private bool IsInTargetPosition() => positionableEntity.GetPosition() == currentTarget;

        private void StopWalking()
        {
            animator.SetBool(GuardAnimatorConstants.IsWalkingParameter, false);
            rigidbody2D.velocity = Vector2.zero;
            spriteRenderer.flipX = false;
        }

        private void Walk()
        {
            const float TargetThreshold = 0.05f;
            const float Speed = 2.5f;
            

            if (Vector2.Distance(positionableEntity.GetPosition(), currentTarget) <= TargetThreshold)
            {
                positionableEntity.SetPosition(currentTarget);
                return;
            }

            var velocity = (currentTarget - positionableEntity.GetPosition()).normalized * Speed;

            rigidbody2D.velocity = velocity;
            spriteRenderer.flipX = velocity.x < -0.1f;
            animator.SetBool(GuardAnimatorConstants.IsWalkingParameter, true);
        }
        #endregion
    }
}
