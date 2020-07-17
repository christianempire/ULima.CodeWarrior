using Assets.Scripts.Constants.Illusion;

namespace Assets.Scripts.Shared.Illusion
{
    public class KillableIllusion : KillableEntity
    {
        protected override string GetAnimatorIsDeadParameter() => IllusionAnimatorConstants.IsDeadParameter;
    }
}
