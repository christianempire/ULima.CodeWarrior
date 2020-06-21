using Assets.Scripts.Shared.Enemy;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Collections.Generic;

namespace Assets.Scripts.Level003
{
    public class Level003APatroller : Patroller
    {
        protected override Queue<CheckpointDirection> GetPatrollingDirections()
        {
            var patrollingDirections = new Queue<CheckpointDirection>();

            patrollingDirections.Enqueue(CheckpointDirection.Up);
            patrollingDirections.Enqueue(CheckpointDirection.Left);
            patrollingDirections.Enqueue(CheckpointDirection.Down);
            patrollingDirections.Enqueue(CheckpointDirection.Right);

            return patrollingDirections;
        }

        protected override float GetPatrollingSpeed() => 1.75f;
    }
}
