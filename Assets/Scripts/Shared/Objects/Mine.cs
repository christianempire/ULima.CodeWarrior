using Assets.Scripts.Constants.Objects;
using Asyncoroutine;
using System.Collections.Generic;
using System.Linq;
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

            if (IsAKillableEntity(collision))
            {
                foreach (var killableEntity in GetNearKillableEntities())
                    killableEntity.TakeDamage(Damage);

                animator.SetTrigger(MineAnimatorConstants.ExplodeParameter);

                await new WaitForSeconds(MineAnimatorConstants.ExplodeAnimationDuration);

                Destroy(gameObject);
            }
        }

        #region Helpers
        private List<KillableEntity> GetNearKillableEntities()
        {
            const float MaxDistance = 5.0f;

            return FindObjectsOfType<KillableEntity>()
                .Where(killableEntity => Vector2.Distance(killableEntity.gameObject.transform.position, transform.position) <= MaxDistance)
                .ToList();
        }

        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
        }

        private bool IsAKillableEntity(Collider2D collision) => collision.GetComponent<KillableEntity>() != null;
        #endregion
    }
}
