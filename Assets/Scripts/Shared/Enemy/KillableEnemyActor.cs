using Assets.Scripts.Constants;

namespace Assets.Scripts.Shared.Enemy
{
    public class KillableEnemyActor : KillableActor
    {
        protected override string GetAnimatorIsDeadParameter() => EnemyAnimatorConstants.IsDeadParameter;
    }
}
