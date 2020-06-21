using UnityEngine;

namespace Assets.Scripts.Shared.Enemy
{
    public class PositionableEnemy : PositionableEntity
    {
        protected override Vector2 GetColliderPositionOffset() => new Vector2(0f, 0.85f);

        protected override Vector2 GetPositionOffset() => new Vector2(0.5f, 1.5f);
    }
}
