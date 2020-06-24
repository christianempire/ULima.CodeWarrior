using Assets.Scripts.Shared.Enemy;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Collections.Generic;

namespace Assets.Scripts.Level003
{
    public class Level003BPatroller : Patroller
    {
        protected override Queue<CheckpointDirection> GetPatrollingDirections()
        {
            var patrollingDirections = new Queue<CheckpointDirection>();

            patrollingDirections.Enqueue(CheckpointDirection.Right);
            patrollingDirections.Enqueue(CheckpointDirection.Right);
            patrollingDirections.Enqueue(CheckpointDirection.Left);
            patrollingDirections.Enqueue(CheckpointDirection.Left);

            return patrollingDirections;
        }

        protected override float GetPatrollingSpeed() => 1.65f;
    }
}
