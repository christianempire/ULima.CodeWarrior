using Assets.Scripts.Constants;
using Assets.Scripts.Shared.Hero;
using Asyncoroutine;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(KillableEnemyActor))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class HeroChaser : MonoBehaviour
    {
        public GameObject Hero;

        #region Properties
        private Animator animator;
        private KillableEnemyActor killableEnemyActor;
        private KillableHeroActor killableHeroActor;
        private bool mustChaseHero;
        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        void Update()
        {
            if (mustChaseHero)
                ChaseHero();
        }

        public async Task ChaseHeroAsync()
        {
            const float ChasedDistance = 1.0f;

            if (killableEnemyActor.IsDead() || killableHeroActor.IsDead())
                return;

            mustChaseHero = true;

            await new WaitUntil(() => HeroIsChased() || killableEnemyActor.IsDead() || killableHeroActor.IsDead());

            mustChaseHero = false;

            StopChasing();

            bool HeroIsChased() => Vector2.Distance(Hero.transform.position, transform.position) <= ChasedDistance;
        }

        #region Helpers
        private void ChaseHero()
        {
            const float Speed = 5.0f;

            var velocity = (Hero.transform.position - transform.position).normalized * Speed;

            rigidbody2D.velocity = velocity;
            spriteRenderer.flipX = velocity.x < -0.1f;
            animator.SetBool(EnemyAnimatorConstants.IsRunningParameter, true);
        }

        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
            killableEnemyActor = GetComponent<KillableEnemyActor>();
            killableHeroActor = Hero.GetComponent<KillableHeroActor>();
            mustChaseHero = false;
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void StopChasing()
        {
            rigidbody2D.velocity = Vector2.zero;
            animator.SetBool(EnemyAnimatorConstants.IsRunningParameter, false);
        }
        #endregion
    }
}
