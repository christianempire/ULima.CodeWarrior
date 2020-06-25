using Assets.Scripts.Constants.Enemy;
using Assets.Scripts.Shared.Hero;
using Asyncoroutine;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(KillableEnemy))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class HeroChaser : MonoBehaviour
    {
        public GameObject Hero;

        #region Properties
        private Animator animator;
        private KillableEnemy killableEnemy;
        private KillableHero killableHero;
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

            if (killableEnemy.IsDead() || killableHero.IsDead())
                return;

            mustChaseHero = true;

            await new WaitUntil(() => HeroIsChased() || killableEnemy.IsDead() || killableHero.IsDead());

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
            killableEnemy = GetComponent<KillableEnemy>();
            killableHero = Hero.GetComponent<KillableHero>();
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
