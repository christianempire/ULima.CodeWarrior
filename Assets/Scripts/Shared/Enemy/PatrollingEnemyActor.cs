using Assets.Scripts.Shared.Hero;
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
        private KillableEnemy killableEnemyActor;
        private KillableHero killableHeroActor;
        private Patroller patroller;
        #endregion

        async void Start()
        {
            InitializeProperties();

            await heroLocator.LocateHeroAsync();
            
            patroller.StopPatrolling();

            while (!killableEnemyActor.IsDead() && !killableHeroActor.IsDead())
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
            killableEnemyActor = GetComponent<KillableEnemy>();
            killableHeroActor = Hero.GetComponent<KillableHero>();
            patroller = GetComponent<Patroller>();
        }
        #endregion
    }
}
