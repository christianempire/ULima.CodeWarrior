using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.CheckpointDirectionStrategies
{
    public abstract class CheckpointDirectionStrategy
    {
        public static List<CheckpointDirectionStrategy> GetStrategies() => new List<CheckpointDirectionStrategy>
        {
            new UpCheckpointDirectionStrategy(),
            new DownCheckpointDirectionStrategy(),
            new LeftCheckpointDirectionStrategy(),
            new RightCheckpointDirectionStrategy()
        };

        public abstract Vector2? GetClosestCheckpointPosition(Vector2 currentCheckpointPosition, List<Vector2> checkpointPositions);

        public abstract bool IsApplicable(CheckpointDirection direction);
    }
}
