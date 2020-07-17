using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.VictoryConditions
{
    public abstract class VictoryCondition : MonoBehaviour
    {
        public abstract Task<bool> IsMetAsync();
    }
}
