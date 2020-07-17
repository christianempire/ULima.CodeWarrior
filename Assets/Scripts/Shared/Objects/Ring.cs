using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.Shared.Objects
{
    public class Ring : MonoBehaviour
    {
        public GameObject Illusion;

        void Awake()
        {
            Illusion.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(TagConstants.HeroTag))
            {
                Illusion.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
