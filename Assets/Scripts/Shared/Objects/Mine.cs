using Assets.Scripts.Constants.Objects;
using Asyncoroutine;
using UnityEngine;

namespace Assets.Scripts.Shared.Objects
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Mine : MonoBehaviour
    {
        #region Properties
        private Animator animator;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        async void OnTriggerEnter2D(Collider2D collision)
        {
            const int Damage = 1000;

            var killableEntity = collision.GetComponent<KillableEntity>();

            if (killableEntity != null)
            {
                killableEntity.TakeDamage(Damage);
                animator.SetTrigger(MineAnimatorConstants.ExplodeParameter);

                await new WaitForSeconds(MineAnimatorConstants.ExplodeAnimationDuration);

                Destroy(gameObject);
            }
        }

        #region Helpers
        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
        }
        #endregion
    }
}
