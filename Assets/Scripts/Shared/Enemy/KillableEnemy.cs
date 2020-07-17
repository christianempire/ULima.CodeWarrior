using Assets.Scripts.Constants.Enemy;

namespace Assets.Scripts.Shared.Enemy
{
    public class KillableEnemy : KillableEntity
    {
        protected override string GetAnimatorIsDeadParameter() => EnemyAnimatorConstants.IsDeadParameter;
    }
}
