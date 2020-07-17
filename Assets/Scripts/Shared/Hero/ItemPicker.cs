using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.Shared.Hero
{
    public class ItemPicker : MonoBehaviour
    {
        #region Properties
        public int ItemsCount { get; private set; }
        #endregion

        void Awake()
        {
            ItemsCount = 0;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag(TagConstants.ItemTag))
                return;

            ItemsCount++;
            Destroy(collision.gameObject);
        }
    }
}
