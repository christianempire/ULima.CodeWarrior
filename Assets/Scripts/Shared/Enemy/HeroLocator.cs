using Assets.Scripts.Constants;
using Assets.Scripts.Shared.Hero;
using Asyncoroutine;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(KillableEnemy))]
    [RequireComponent(typeof(PositionableEntity))]
    public class HeroLocator : MonoBehaviour
    {
        public GameObject Hero;
        public LayerMask CurrentLayerMask;

        #region Properties
        private PositionableEntity heroPositionableEntity;
        private bool isHeroVisible;
        private KillableEnemy killableEnemyActor;
        private KillableHero killableHeroActor;
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
            if (killableEnemyActor.IsDead() || killableHeroActor.IsDead())
                return;

            mustLocateHero = true;

            await new WaitUntil(() => isHeroVisible || killableEnemyActor.IsDead() || killableHeroActor.IsDead());

            mustLocateHero = false;
        }

        #region Helpers
        private void InitializeProperties()
        {
            heroPositionableEntity = Hero.GetComponent<PositionableEntity>();
            isHeroVisible = false;
            killableEnemyActor = GetComponent<KillableEnemy>();
            killableHeroActor = Hero.GetComponent<KillableHero>();
            mustLocateHero = false;
            positionableEntity = GetComponent<PositionableEntity>();
        }

        private void LocateHero()
        {
            const float maxDistance = 1000.0f;

            var heroDirection = heroPositionableEntity.GetColliderPosition() - positionableEntity.GetColliderPosition();
            var raycastHit = Physics2D.Raycast(positionableEntity.GetColliderPosition(), heroDirection, maxDistance, ~CurrentLayerMask);

            isHeroVisible = raycastHit.collider != null && raycastHit.collider.CompareTag(TagConstants.PlayerTag);
        }
        #endregion
    }
}
