using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Guard;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Level006
{
    [RequireComponent(typeof(FreeWalker))]
    public class Level006GuardHeroListener : HeroListener
    {
        #region Properties
        private FreeWalker freeWalker;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        public override async Task OnHeroSpeakAsync(string message)
        {
            const string Password = "Achoo";

            if (message.Equals(Password))
                await freeWalker.WalkAsync(Vector2.left * 2);
        }

        #region Helpers
        private void InitializeProperties()
        {
            freeWalker = GetComponent<FreeWalker>();
        }
        #endregion
    }
}
