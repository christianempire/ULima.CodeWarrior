using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.CheckpointDirectionStrategies
{
    public class LeftCheckpointDirectionStrategy : CheckpointDirectionStrategy
    {
        public override Vector2? GetClosestCheckpointPosition(Vector2 currentCheckpointPosition, List<Vector2> checkpointPositions) =>
            checkpointPositions
                .Where(position => position != currentCheckpointPosition && position.y == currentCheckpointPosition.y && position.x < currentCheckpointPosition.x)
                .Select(position => new
                {
                    Position = position,
                    DistanceToCurrentPosition = Vector3.Distance(currentCheckpointPosition, position)
                })
                .OrderBy(positionInfo => positionInfo.DistanceToCurrentPosition)
                .FirstOrDefault()?
                .Position;

        public override bool IsApplicable(CheckpointDirection direction) => direction == CheckpointDirection.Left;
    }
}
