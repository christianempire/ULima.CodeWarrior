using Assets.Scripts.Shared.Prisoner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.VictoryConditions
{
    public class PrisonersVictoryCondition : VictoryCondition
    {
        public List<GameObject> Prisoners;

        #region Properties
        private List<BasicPrisonerActor> basicPrisonerActors;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        public override Task<bool> IsMetAsync() => Task.FromResult(basicPrisonerActors.All(basicPrisonerActor => basicPrisonerActor.IsRescued()));

        #region Helpers
        private void InitializeProperties()
        {
            basicPrisonerActors = Prisoners.Select(prisoner => prisoner.GetComponent<BasicPrisonerActor>()).ToList();
        }
        #endregion
    }
}
