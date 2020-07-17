using Assets.Scripts.Constants;
using Assets.Scripts.Shared.Hero;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.VictoryConditions
{
    public class ItemsVictoryCondition : VictoryCondition
    {
        public GameObject Hero;

        #region Properties
        private ItemPicker heroItemPicker;
        private int totalItems;
        #endregion

        public void Awake()
        {
            InitializeProperties();
        }

        public override async Task<bool> IsMetAsync() => await Task.FromResult(heroItemPicker.ItemsCount == totalItems);

        #region Helpers
        private void InitializeProperties()
        {
            heroItemPicker = Hero.GetComponent<ItemPicker>();
            totalItems = GameObject.FindGameObjectsWithTag(TagConstants.ItemTag).Length;
        }
        #endregion
    }
}
