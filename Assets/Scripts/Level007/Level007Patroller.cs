using Assets.Scripts.Shared.Enemy;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Collections.Generic;

namespace Assets.Scripts.Level007
{
    public class Level007Patroller : Patroller
    {
        protected override Queue<CheckpointDirection> GetPatrollingDirections()
        {
            var patrollingDirections = new Queue<CheckpointDirection>();

            patrollingDirections.Enqueue(CheckpointDirection.Left);
            patrollingDirections.Enqueue(CheckpointDirection.Up);
            patrollingDirections.Enqueue(CheckpointDirection.Right);
            patrollingDirections.Enqueue(CheckpointDirection.Down);

            return patrollingDirections;
        }

        protected override float GetPatrollingSpeed() => 1.75f;
    }
}
