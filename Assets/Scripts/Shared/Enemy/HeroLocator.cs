using Assets.Scripts.Constants;
using Assets.Scripts.Shared.Hero;
using Asyncoroutine;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(KillableEnemyActor))]
    public class HeroLocator : MonoBehaviour
    {
        public GameObject Hero;

        #region Properties
        private bool isHeroVisible;
        private KillableEnemyActor killableEnemyActor;
        private KillableHeroActor killableHeroActor;
        private bool mustLocateHero;
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
            isHeroVisible = false;
            killableEnemyActor = GetComponent<KillableEnemyActor>();
            killableHeroActor = Hero.GetComponent<KillableHeroActor>();
            mustLocateHero = false;
        }

        private void LocateHero()
        {
            var heroDirection = Hero.transform.position - transform.position;
            var raycastHit = Physics2D.Raycast(transform.position, heroDirection);

            isHeroVisible = raycastHit.collider != null && raycastHit.collider.CompareTag(TagConstants.PlayerTag);
        }
        #endregion
    }
}
