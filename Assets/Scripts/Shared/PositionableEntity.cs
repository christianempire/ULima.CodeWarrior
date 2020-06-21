using UnityEngine;

namespace Assets.Scripts.Shared
{
    public abstract class PositionableEntity : MonoBehaviour
    {
        public Vector2 GetColliderPosition()
        {
            return new Vector2(transform.position.x, transform.position.y) - GetColliderPositionOffset();
        }

        public Vector2 GetPosition()
        {
            return new Vector2(transform.position.x, transform.position.y) - GetPositionOffset();
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position + GetPositionOffset();
        }

        protected abstract Vector2 GetColliderPositionOffset();

        protected abstract Vector2 GetPositionOffset();
    }
}
