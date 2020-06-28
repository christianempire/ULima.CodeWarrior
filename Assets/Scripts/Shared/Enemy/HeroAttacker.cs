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
        private const float TIME_BEFORE_ATTACKING = 0.16f;
        private const float TIME_AFTER_ATTACKING = 0.57f;

        private Animator animator;
        private KillableEntity killableEntity;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        public async Task AttackHeroAsync()
        {
            const int Damage = 150;

            if (killableEntity.IsDead())
                return;

            animator.SetTrigger(EnemyAnimatorConstants.AttackParameter);

            await new WaitForSeconds(TIME_BEFORE_ATTACKING);

            if (!killableEntity.IsDead())
                GetClosestKillableHeroEntity().TakeDamage(Damage);

            await new WaitForSeconds(TIME_AFTER_ATTACKING);
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
            .First()
            .KillableHeroEntity;

        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
            killableEntity = GetComponent<KillableEntity>();
        }
        #endregion
    }
}
