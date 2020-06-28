using Assets.Scripts.Constants;
using Assets.Scripts.Constants.Enemy;
using Asyncoroutine;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(KillableEntity))]
    public class HeroAttacker : MonoBehaviour
    {
        #region Properties
        private Animator animator;
        private KillableEntity closestKillableHeroEntity;
        private KillableEntity killableEntity;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        void Update()
        {
            closestKillableHeroEntity = GetClosestKillableHeroEntity();
        }

        public async Task AttackHeroAsync()
        {
            const int Damage = 150;
            const float TimeBeforeAttacking = 0.16f;
            const float TimeAfterAttacking = 0.57f;

            if (killableEntity.IsDead())
                return;

            animator.SetTrigger(EnemyAnimatorConstants.AttackParameter);

            await new WaitForSeconds(TimeBeforeAttacking);

            if (!killableEntity.IsDead() && closestKillableHeroEntity != null)
            {
                closestKillableHeroEntity.TakeDamage(Damage);

                await new WaitForSeconds(TimeAfterAttacking);
            }
        }

        #region Helpers
        private KillableEntity GetClosestKillableHeroEntity() => GameObject.FindGameObjectsWithTag(TagConstants.HeroTag)
            .Select(heroObject => new
            {
                KillableHeroEntity = heroObject.GetComponent<KillableEntity>(),
                Distance = Vector2.Distance(heroObject.transform.position, transform.position)
            })
            .Where(heroObjectData => !heroObjectData.KillableHeroEntity.IsDead())
            .OrderBy(heroObjectData => heroObjectData.Distance)
            .FirstOrDefault()?
            .KillableHeroEntity;

        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
            killableEntity = GetComponent<KillableEntity>();
        }
        #endregion
    }
}
