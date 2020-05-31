﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Level001Scripts.SeekerDirectionStrategies
{
    public class RightSeekerDirectionStrategy : ISeekerDirectionStrategy
    {
        public Vector2? GetClosestCheckpointPosition(Vector2 currentCheckpointPosition, List<Vector2> checkpointPositions) =>
            checkpointPositions
                .Where(position => position != currentCheckpointPosition && position.y == currentCheckpointPosition.y && position.x > currentCheckpointPosition.x)
                .Select(position => new
                {
                    Position = position,
                    DistanceToCurrentPosition = Vector3.Distance(currentCheckpointPosition, position)
                })
                .OrderBy(positionInfo => positionInfo.DistanceToCurrentPosition)
                .FirstOrDefault()?
                .Position;

        public bool IsApplicable(SeekerDirection direction) => direction == SeekerDirection.Right;
    }
}
