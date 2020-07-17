using Assets.Scripts.Constants.Guard;

namespace Assets.Scripts.Shared.Guard
{
    public class KillableGuard : KillableEntity
    {
        protected override string GetAnimatorIsDeadParameter() => GuardAnimatorConstants.IsDeadParameter;
    }
}
