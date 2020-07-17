using UnityEngine;

namespace Assets.Scripts.Shared.Illusion
{
    public class PositionableIllusion : PositionableEntity
    {
        protected override Vector2 GetColliderPositionOffset() => new Vector2(0f, 1f);

        protected override Vector2 GetPositionOffset() => new Vector2(0.5f, 1.5f);
    }
}
