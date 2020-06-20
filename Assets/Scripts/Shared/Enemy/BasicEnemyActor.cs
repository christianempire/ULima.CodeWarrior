using Assets.Scripts.Shared.Hero;
using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    [RequireComponent(typeof(HeroAttacker))]
    [RequireComponent(typeof(HeroChaser))]
    [RequireComponent(typeof(HeroLocator))]
    public class BasicEnemyActor : MonoBehaviour
    {
        public GameObject Hero;

        #region Properties
        private HeroAttacker heroAttacker;
        private HeroChaser heroChaser;
        private HeroLocator heroLocator;
        private KillableEnemyActor killableEnemyActor;
        private KillableHeroActor killableHeroActor;
        #endregion

        async void Start()
        {
            InitializeProperties();

            await heroLocator.LocateHeroAsync();

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
            killableEnemyActor = GetComponent<KillableEnemyActor>();
            killableHeroActor = Hero.GetComponent<KillableHeroActor>();
        }
        #endregion
    }
}
