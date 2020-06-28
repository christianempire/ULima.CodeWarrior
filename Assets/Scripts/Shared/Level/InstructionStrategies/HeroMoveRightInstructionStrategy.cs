using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public class HeroMoveRightInstructionStrategy : InstructionStrategy
    {
        private const string BaseInstruction = "HeroMoveRight";

        private readonly CheckpointSeeker checkpointSeeker;

        public static string GetFormattedInstruction() => BaseInstruction;

        public HeroMoveRightInstructionStrategy(GameObject hero) : base(hero)
        {
            checkpointSeeker = hero.GetComponent<CheckpointSeeker>();
        }

        public override async Task ExecuteInstruction(string instruction) => await checkpointSeeker.SeekCheckpointAsync(CheckpointDirection.Right);

        public override string GetLogMessage(string instruction) => "Going right";

        public override bool IsApplicable(string instruction) => instruction.Equals(BaseInstruction);
    }
}
