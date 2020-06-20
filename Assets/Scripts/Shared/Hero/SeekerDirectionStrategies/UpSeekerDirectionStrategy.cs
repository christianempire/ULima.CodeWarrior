using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Shared.Hero.SeekerDirectionStrategies
{
    public class UpSeekerDirectionStrategy : ISeekerDirectionStrategy
    {
        public Vector2? GetClosestCheckpointPosition(Vector2 currentCheckpointPosition, List<Vector2> checkpointPositions) =>
            checkpointPositions
                .Where(position => position != currentCheckpointPosition && position.x == currentCheckpointPosition.x && position.y > currentCheckpointPosition.y)
                .Select(position => new
                {
                    Position = position,
                    DistanceToCurrentPosition = Vector3.Distance(currentCheckpointPosition, position)
                })
                .OrderBy(positionInfo => positionInfo.DistanceToCurrentPosition)
                .FirstOrDefault()?
                .Position;

        public bool IsApplicable(SeekerDirection direction) => direction == SeekerDirection.Up;
    }
}
