using Asyncoroutine;
using UnityEngine;

namespace Assets.Scripts.Shared.Prisoner
{
    [RequireComponent(typeof(HeroFollower))]
    public class BasicPrisonerActor : MonoBehaviour
    {
        public GameObject Guard;

        #region Properties
        private HeroFollower heroFollower;
        private KillableEntity killableGuard;
        #endregion

        async void Start()
        {
            InitializeProperties();

            await new WaitUntil(() => killableGuard.IsDead());

            heroFollower.MustFollowHero = true;
        }

        public bool IsRescued() => killableGuard.IsDead();

        #region Helpers
        private void InitializeProperties()
        {
            heroFollower = GetComponent<HeroFollower>();
            killableGuard = Guard.GetComponent<KillableEntity>();
        }
        #endregion
    }
}
