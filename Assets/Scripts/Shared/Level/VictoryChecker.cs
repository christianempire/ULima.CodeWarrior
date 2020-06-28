using Assets.Scripts.Shared.Level.VictoryConditions;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level
{
    public class VictoryChecker : MonoBehaviour
    {
        public GameObject VictoryConditions;

        public async Task<bool> IsVictoryAchievedAsync()
        {
            foreach (var victoryCondition in VictoryConditions.GetComponents<VictoryCondition>())
                if (!await victoryCondition.IsMetAsync())
                    return false;

            return true;
        }
    }
}
