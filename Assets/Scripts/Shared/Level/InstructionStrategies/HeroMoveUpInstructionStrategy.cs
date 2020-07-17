using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public class HeroMoveUpInstructionStrategy : InstructionStrategy
    {
        private const string BaseInstruction = "HeroMoveUp";

        private readonly CheckpointSeeker checkpointSeeker;

        public static string GetFormattedInstruction() => BaseInstruction;

        public HeroMoveUpInstructionStrategy(GameObject hero) : base(hero)
        {
            checkpointSeeker = hero.GetComponent<CheckpointSeeker>();
        }

        public override async Task ExecuteInstruction(string instruction) => await checkpointSeeker.SeekCheckpointAsync(CheckpointDirection.Up);

        public override string GetLogMessage(string instruction) => "Going up";

        public override bool IsApplicable(string instruction) => instruction.Equals(BaseInstruction);
    }
}
