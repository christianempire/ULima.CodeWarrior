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
        private const float TIME_BEFORE_ATTACKING = 0.16f;
        private const float TIME_AFTER_ATTACKING = 0.2f;

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

            if (killableEntity.IsDead())
                return;

            var attackingKillableEntity = FindKillableEntity(name);

            if (attackingKillableEntity == null)
                return;

            animator.SetTrigger(HeroAnimatorConstants.AttackParameter);

            await new WaitForSeconds(TIME_BEFORE_ATTACKING);

            if (!killableEntity.IsDead())
                attackingKillableEntity.TakeDamage(Damage);

            await new WaitForSeconds(TIME_AFTER_ATTACKING);
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
