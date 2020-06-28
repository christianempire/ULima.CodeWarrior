using Assets.Scripts.Constants.Hero;
using Asyncoroutine;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Hero
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(KillableEntity))]
    public class Attacker : MonoBehaviour
    {
        #region Properties
        private Animator animator;
        private KillableEntity killableEntity;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        public async Task AttackEntityAsync(string name)
        {
            const int Damage = 150;
            const float TimeBeforeAttacking = 0.16f;
            const float TimeAfterAttacking = 0.2f;

            if (killableEntity.IsDead())
                return;

            var attackingKillableEntity = FindKillableEntity(name);

            if (attackingKillableEntity == null)
                return;

            animator.SetTrigger(HeroAnimatorConstants.AttackParameter);

            await new WaitForSeconds(TimeBeforeAttacking);

            if (!killableEntity.IsDead())
            {
                attackingKillableEntity.TakeDamage(Damage);

                await new WaitForSeconds(TimeAfterAttacking);
            }
        }

        #region Helpers
        private KillableEntity FindKillableEntity(string name)
        {
            var obj = GameObject.Find(name);

            if (obj == null)
                return null;

            return obj.GetComponent<KillableEntity>();
        }

        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
            killableEntity = GetComponent<KillableEntity>();
        }
        #endregion
    }
}
