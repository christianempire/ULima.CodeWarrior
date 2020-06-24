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
        private KillableEnemy killableEnemy;
        private KillableHero killableHero;
        #endregion

        async void Start()
        {
            InitializeProperties();

            await heroLocator.LocateHeroAsync();

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
            killableEnemy = GetComponent<KillableEnemy>();
            killableHero = Hero.GetComponent<KillableHero>();
        }
        #endregion
    }
}
