using Assets.Scripts.Constants.Hero;
using Asyncoroutine;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Hero
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(KillableEntity))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PositionableEntity))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Chaser : MonoBehaviour
    {
        public LayerMask CurrentLayerMask;

        #region Properties
        private Animator animator;
        private PositionableEntity chasingPositionableEntity;
        private KillableEntity killableEntity;
        private bool mustChaseEntity;
        private PositionableEntity positionableEntity;
        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        void Update()
        {
            if (mustChaseEntity)
                ChaseEntity();
        }

        public async Task ChaseEntityAsync(string name)
        {
            const float ChasedDistance = 1.0f;

            if (killableEntity.IsDead())
                return;

            chasingPositionableEntity = FindPositionableEntity(name);

            if (chasingPositionableEntity == null)
                return;

            mustChaseEntity = true;

            await new WaitUntil(() => EntityIsChased() || killableEntity.IsDead());

            mustChaseEntity = false;

            StopChasing();

            bool EntityIsChased() => Vector2.Distance(chasingPositionableEntity.transform.position, transform.position) <= ChasedDistance;
        }

        public bool IsEntityVisible(string name)
        {
            const float MaxDistance = 1000.0f;

            chasingPositionableEntity = FindPositionableEntity(name);

            if (chasingPositionableEntity == null)
                return false;

            var chasingEntityDirection = chasingPositionableEntity.GetColliderPosition() - positionableEntity.GetColliderPosition();
            var raycastHit = Physics2D.Raycast(positionableEntity.GetColliderPosition(), chasingEntityDirection, MaxDistance, ~CurrentLayerMask);

            return raycastHit.collider != null;
        }

        #region Helpers
        private void ChaseEntity()
        {
            const float Speed = 2.5f;

            var velocity = (chasingPositionableEntity.transform.position - transform.position).normalized * Speed;

            rigidbody2D.velocity = velocity;
            spriteRenderer.flipX = velocity.x < -0.1f;
            animator.SetBool(HeroAnimatorConstants.IsWalkingParameter, true);
        }

        private PositionableEntity FindPositionableEntity(string name)
        {
            var obj = GameObject.Find(name);

            if (obj == null)
                return null;

            return obj.GetComponent<PositionableEntity>();
        }

        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
            killableEntity = GetComponent<KillableEntity>();
            mustChaseEntity = false;
            positionableEntity = GetComponent<PositionableEntity>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void StopChasing()
        {
            rigidbody2D.velocity = Vector2.zero;
            animator.SetBool(HeroAnimatorConstants.IsWalkingParameter, false);
        }
        #endregion
    }
}
