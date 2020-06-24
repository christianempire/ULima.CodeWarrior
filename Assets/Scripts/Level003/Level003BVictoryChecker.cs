using Assets.Scripts.Constants;
using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level;
using Asyncoroutine;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Level003
{
    public class Level003BVictoryChecker : VictoryChecker
    {
        public GameObject Hero;

        #region Properties
        private ItemPicker heroItemPicker;
        private KillableHero killableHero;
        private int totalItems;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        public override async Task<bool> IsVictoryAchievedAsync()
        {
            const float StayAliveTime = 2.0f;

            await new WaitForSeconds(StayAliveTime);

            return HasCollectedAllItems() && !killableHero.IsDead();

            bool HasCollectedAllItems() => heroItemPicker.ItemsCount == totalItems;
        }

        #region Helpers
        private void InitializeProperties()
        {
            heroItemPicker = Hero.GetComponent<ItemPicker>();
            killableHero = Hero.GetComponent<KillableHero>();
            totalItems = GameObject.FindGameObjectsWithTag(TagConstants.ItemTag).Length;
        }
        #endregion
    }
}
