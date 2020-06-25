using Assets.Scripts.Constants.Hero;

namespace Assets.Scripts.Shared.Hero
{
    public class KillableHero : KillableEntity
    {
        protected override string GetAnimatorIsDeadParameter() => HeroAnimatorConstants.IsDeadParameter;
    }
}
