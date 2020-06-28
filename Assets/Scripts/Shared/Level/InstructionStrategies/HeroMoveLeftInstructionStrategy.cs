using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public class HeroMoveLeftInstructionStrategy : InstructionStrategy
    {
        private const string BaseInstruction = "HeroMoveLeft";

        private readonly CheckpointSeeker checkpointSeeker;

        public static string GetFormattedInstruction() => BaseInstruction;

        public HeroMoveLeftInstructionStrategy(GameObject hero) : base(hero)
        {
            checkpointSeeker = hero.GetComponent<CheckpointSeeker>();
        }

        public override async Task ExecuteInstruction(string instruction) => await checkpointSeeker.SeekCheckpointAsync(CheckpointDirection.Left);

        public override string GetLogMessage(string instruction) => "Going left";

        public override bool IsApplicable(string instruction) => instruction.Equals(BaseInstruction);
    }
}
