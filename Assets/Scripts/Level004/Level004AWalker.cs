using Assets.Scripts.Constants.Illusion;
using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Collections.Generic;

namespace Assets.Scripts.Level004
{
    public class Level004AWalker : Walker
    {
        protected override string GetAnimatorIsWalkingParameter() => IllusionAnimatorConstants.IsWalkingParameter;

        protected override Queue<CheckpointDirection> GetWalkingDirections()
        {
            var walkingDirections = new Queue<CheckpointDirection>();

            walkingDirections.Enqueue(CheckpointDirection.Right);
            walkingDirections.Enqueue(CheckpointDirection.Right);

            return walkingDirections;
        }

        protected override float GetWalkingSpeed() => 2.5f;
    }
}
