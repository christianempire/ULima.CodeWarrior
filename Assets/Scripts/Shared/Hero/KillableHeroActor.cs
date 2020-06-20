using Assets.Scripts.Constants;

namespace Assets.Scripts.Shared.Hero
{
    public class KillableHeroActor : KillableActor
    {
        protected override string GetAnimatorIsDeadParameter() => HeroAnimatorConstants.IsDeadParameter;
    }
}
