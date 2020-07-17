using Assets.Scripts.Constants;
using Asyncoroutine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.VictoryConditions
{
    public class StayAliveVictoryCondition : VictoryCondition
    {
        public GameObject Hero;

        #region Properties
        private List<KillableEntity> enemiesKillableEntities;
        private KillableEntity heroKillableEntity;
        #endregion

        public void Awake()
        {
            InitializeProperties();
        }

        public override async Task<bool> IsMetAsync()
        {
            await new WaitUntil(() => heroKillableEntity.IsDead() ||
                enemiesKillableEntities.All(enemyKillableEntity => enemyKillableEntity.IsDead()));

            return !heroKillableEntity.IsDead();
        }

        #region Helpers
        private void InitializeProperties()
        {
            heroKillableEntity = Hero.GetComponent<KillableEntity>();
            enemiesKillableEntities = GameObject.FindGameObjectsWithTag(TagConstants.EnemyTag)
                .Select(enemyObject => enemyObject.GetComponent<KillableEntity>())
                .ToList();
        }
        #endregion
    }
}
