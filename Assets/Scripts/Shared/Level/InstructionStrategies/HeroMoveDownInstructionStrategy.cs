using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public class HeroMoveDownInstructionStrategy : InstructionStrategy
    {
        private const string BaseInstruction = "HeroMoveDown";

        private readonly CheckpointSeeker checkpointSeeker;

        public static string GetFormattedInstruction() => BaseInstruction;

        public HeroMoveDownInstructionStrategy(GameObject hero) : base(hero)
        {
            checkpointSeeker = hero.GetComponent<CheckpointSeeker>();
        }

        public override async Task ExecuteInstruction(string instruction) => await checkpointSeeker.SeekCheckpointAsync(CheckpointDirection.Down);

        public override string GetLogMessage(string instruction) => "Going down";

        public override bool IsApplicable(string instruction) => instruction.Equals(BaseInstruction);
    }
}
