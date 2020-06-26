using Assets.Scripts.Constants;
using Asyncoroutine;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(KillableEnemy))]
    [RequireComponent(typeof(PositionableEntity))]
    public class HeroLocator : MonoBehaviour
    {
        public LayerMask CurrentLayerMask;

        #region Properties
        private bool isHeroVisible;
        private KillableEnemy killableEnemy;
        private bool mustLocateHero;
        private PositionableEntity positionableEntity;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        void Update()
        {
            if (mustLocateHero)
                LocateHero();
        }

        public async Task LocateHeroAsync()
        {
            if (killableEnemy.IsDead())
                return;

            mustLocateHero = true;

            await new WaitUntil(() => isHeroVisible || killableEnemy.IsDead());

            mustLocateHero = false;
        }

        #region Helpers
        private void InitializeProperties()
        {
            isHeroVisible = false;
            killableEnemy = GetComponent<KillableEnemy>();
            mustLocateHero = false;
            positionableEntity = GetComponent<PositionableEntity>();
        }

        private void LocateHero()
        {
            const float maxDistance = 1000.0f;

            var raycastHitColliders = GameObject.FindGameObjectsWithTag(TagConstants.HeroTag)
                .Select(heroObject =>
                {
                    var heroPositionableEntity = heroObject.GetComponent<PositionableEntity>();
                    var heroDirection = heroPositionableEntity.GetColliderPosition() - positionableEntity.GetColliderPosition();
                    var raycastHit = Physics2D.Raycast(positionableEntity.GetColliderPosition(), heroDirection, maxDistance, ~CurrentLayerMask);

                    return raycastHit.collider;
                })
                .ToList();

            isHeroVisible = raycastHitColliders.Any(collider => collider != null && 
                collider.CompareTag(TagConstants.HeroTag) &&
                !collider.GetComponent<KillableEntity>().IsDead());
        }
        #endregion
    }
}
