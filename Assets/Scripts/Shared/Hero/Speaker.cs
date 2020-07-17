using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Hero
{
    public class Speaker : MonoBehaviour
    {
        #region Properties
        private KillableEntity killableEntity;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        public async Task SayAsync(string message)
        {
            if (!killableEntity.IsDead())
                foreach (var heroListener in FindObjectsOfType<HeroListener>())
                     await heroListener.OnHeroSpeakAsync(message);
        }

        #region Helpers
        private void InitializeProperties()
        {
            killableEntity = GetComponent<KillableEntity>();
        }
        #endregion
    }
}
