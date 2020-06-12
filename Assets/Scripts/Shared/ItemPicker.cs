using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.Shared
{
    public class ItemPicker : MonoBehaviour
    {
        #region Properties
        public int ItemsCount { get; private set; }
        #endregion

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag(TagConstants.ItemTag))
                return;

            ItemsCount++;
            Destroy(collision.gameObject);
        }

        void Start()
        {
            ItemsCount = 0;
        }
    }
}
