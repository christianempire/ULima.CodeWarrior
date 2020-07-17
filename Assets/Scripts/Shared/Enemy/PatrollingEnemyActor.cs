using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(HeroAttacker))]
    [RequireComponent(typeof(HeroChaser))]
    [RequireComponent(typeof(HeroLocator))]
    public class PatrollingEnemyActor : MonoBehaviour
    {
        public GameObject Hero;

        #region Properties
        private HeroAttacker heroAttacker;
        private HeroChaser heroChaser;
        private HeroLocator heroLocator;
        private KillableEntity killableEnemy;
        private KillableEntity killableHero;
        private Patroller patroller;
        #endregion

        async void Start()
        {
            InitializeProperties();

            await heroLocator.LocateHeroAsync();
            
            patroller.StopPatrolling();

            while (!killableEnemy.IsDead() && !killableHero.IsDead())
            {
                await heroChaser.ChaseHeroAsync();
                await heroAttacker.AttackHeroAsync();
            }
        }

        #region Helpers
        private void InitializeProperties()
        {
            heroAttacker = GetComponent<HeroAttacker>();
            heroChaser = GetComponent<HeroChaser>();
            heroLocator = GetComponent<HeroLocator>();
            killableEnemy = GetComponent<KillableEntity>();
            killableHero = Hero.GetComponent<KillableEntity>();
            patroller = GetComponent<Patroller>();
        }
        #endregion
    }
}
