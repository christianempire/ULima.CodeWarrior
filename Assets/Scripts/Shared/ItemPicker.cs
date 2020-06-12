using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shared
{
    public class ItemPicker : MonoBehaviour
    {
        #region Properties
        private Dictionary<string, int> itemQuantities;
        #endregion
        
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag(TagConstants.ItemTag))
                return;

            if (itemQuantities.ContainsKey(collision.gameObject.name))
                itemQuantities[collision.gameObject.name]++;
            else
                itemQuantities[collision.gameObject.name] = 1;

            Destroy(collision.gameObject);
        }

        void Start()
        {
            itemQuantities = new Dictionary<string, int>();
        }

        public int GetPickedItemQuantity(string itemName)
        {
            if (itemQuantities.TryGetValue(itemName, out var itemQuantity))
                return itemQuantity;
            else
                return 0;
        }
    }
}
