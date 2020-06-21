using Assets.Scripts.Constants;

namespace Assets.Scripts.Shared.Hero
{
    public class KillableHero : KillableEntity
    {
        protected override string GetAnimatorIsDeadParameter() => HeroAnimatorConstants.IsDeadParameter;
    }
}
