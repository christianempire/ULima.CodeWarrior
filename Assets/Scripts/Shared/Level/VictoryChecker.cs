using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level
{
    public abstract class VictoryChecker : MonoBehaviour
    {
        public abstract Task<bool> IsVictoryAchievedAsync();
    }
}
