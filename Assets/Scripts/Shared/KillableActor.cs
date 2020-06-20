using UnityEngine;

namespace Assets.Scripts.Shared
{
    [RequireComponent(typeof(Animator))]
    public abstract class KillableActor : MonoBehaviour
    {
        public int HitPoints = 300;
        public int MaxHitPoints = 300;

        #region Properties
        private const int MinHitPoints = 0;

        private Animator animator;
        #endregion

        void Awake()
        {
            InitializeProperties();
            ClampHitPoints();
        }

        public bool IsDead()
        {
            return HitPoints == MinHitPoints;
        }

        public void Kill()
        {
            if (IsDead())
                return;

            HitPoints = MinHitPoints;
            animator.SetBool(GetAnimatorIsDeadParameter(), true);
        }

        public void TakeDamage(int damage)
        {
            HitPoints = Mathf.Max(HitPoints - damage, MinHitPoints);

            if (IsDead())
                animator.SetBool(GetAnimatorIsDeadParameter(), true);
        }

        protected abstract string GetAnimatorIsDeadParameter();

        #region Helpers
        private void ClampHitPoints()
        {
            Mathf.Clamp(MaxHitPoints, MinHitPoints, int.MaxValue);
            Mathf.Clamp(HitPoints, MinHitPoints, MaxHitPoints);
        }

        private void InitializeProperties()
        {
            animator = GetComponent<Animator>();
        }
        #endregion
    }
}
