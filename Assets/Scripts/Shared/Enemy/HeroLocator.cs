using Assets.Scripts.Constants;
using Asyncoroutine;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(KillableEntity))]
    [RequireComponent(typeof(PositionableEntity))]
    public class HeroLocator : MonoBehaviour
    {
        public LayerMask CurrentLayerMask;

        #region Properties
        private bool isHeroVisible;
        private KillableEntity killableEntity;
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
            if (killableEntity.IsDead())
                return;

            mustLocateHero = true;

            await new WaitUntil(() => isHeroVisible || killableEntity.IsDead());

            mustLocateHero = false;
        }

        #region Helpers
        private void InitializeProperties()
        {
            isHeroVisible = false;
            killableEntity = GetComponent<KillableEntity>();
            mustLocateHero = false;
            positionableEntity = GetComponent<PositionableEntity>();
        }

        private void LocateHero()
        {
            const float MaxDistance = 1000.0f;

            var raycastHitColliders = GameObject.FindGameObjectsWithTag(TagConstants.HeroTag)
                .Select(heroObject =>
                {
                    var heroPositionableEntity = heroObject.GetComponent<PositionableEntity>();
                    var heroDirection = heroPositionableEntity.GetColliderPosition() - positionableEntity.GetColliderPosition();
                    var raycastHit = Physics2D.Raycast(positionableEntity.GetColliderPosition(), heroDirection, MaxDistance, ~CurrentLayerMask);

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
