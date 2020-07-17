using UnityEngine;

namespace Assets.Scripts.Shared.Prisoner
{
    public class PositionablePrisoner : PositionableEntity
    {
        protected override Vector2 GetColliderPositionOffset() => new Vector2(0f, 0f);

        protected override Vector2 GetPositionOffset() => new Vector2(0f, 0f);
    }
}
