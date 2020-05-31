using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SharedLevelScripts.SeekerDirectionStrategies
{
    public interface ISeekerDirectionStrategy
    {
        Vector2? GetClosestCheckpointPosition(Vector2 currentCheckpointPosition, List<Vector2> checkpointPositions);
        bool IsApplicable(SeekerDirection direction);
    }
}
