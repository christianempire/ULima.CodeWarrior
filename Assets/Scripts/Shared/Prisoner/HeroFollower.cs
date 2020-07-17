using Assets.Scripts.Constants.Prisoner;
using UnityEngine;

namespace Assets.Scripts.Shared.Prisoner
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class HeroFollower : MonoBehaviour
    {
        public GameObject Hero;
        public bool MustFollowHero;

        #region Properties
        private Animator animator;
        private PositionableEntity positionableHero;
        private PositionableEntity positionablePrisoner;
        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;
        #endregion

        public void Awake()
        {
            InitializeProperties();
        }

        public void Update()
        {
            if (MustFollowHero && !HeroIsFollowed())
                FollowHero();
            else
                StopFollowing();
        }

        #region Helpers
        private void FollowHero()
        {
            const float MaxSpeed = 5f;
            const float MinSpeed = 2f;
            const float SpeedIncreaseThreshold = 2f;

            var distance = Vector2.Distance(positionableHero.GetColliderPosition(), positionablePrisoner.GetColliderPosition());
            var velocity = (positionableHero.GetColliderPosition() - positionablePrisoner.GetColliderPosition()).normalized;

            if (distance <= SpeedIncreaseThreshold)
                velocity *= MinSpeed;
            else
                velocity *= MaxSpeed;
            
            rigidbody2D.velocity = velocity;
            spriteRenderer.flipX = velocity.x < -0.1f;
            animator.SetBool(PrisonerAnimatorConstants.IsRunningParameter, true);
        }

        private bool HeroIsFollowed()
        {
            const float FollowedDistance = 0.75f;

            return Vector2.Distance(positionableHero.GetPosition(), positionablePrisoner.GetPosition()) <= FollowedDistance;
        }

        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
            positionableHero = Hero.GetComponent<PositionableEntity>();
            positionablePrisoner = GetComponent<PositionableEntity>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void StopFollowing()
        {
            rigidbody2D.velocity = Vector2.zero;
            animator.SetBool(PrisonerAnimatorConstants.IsRunningParameter, false);
        }
        #endregion
    }
}
