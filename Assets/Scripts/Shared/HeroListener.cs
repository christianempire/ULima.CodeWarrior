using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared
{
    public abstract class HeroListener : MonoBehaviour
    {
        public abstract Task OnHeroSpeakAsync(string message);
    }
}
