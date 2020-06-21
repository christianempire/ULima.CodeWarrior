using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Threading.Tasks;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public class HeroMoveUpLevelInstructionStrategy : InstructionStrategy
    {
        public const string Instruction = "HeroMoveUp";

        private readonly CheckpointSeeker checkpointSeeker;

        public HeroMoveUpLevelInstructionStrategy(CheckpointSeeker checkpointSeeker)
        {
            this.checkpointSeeker = checkpointSeeker;
        }

        public override async Task ExecuteInstruction(string instruction) => await checkpointSeeker.SeekCheckpointAsync(CheckpointDirection.Up);

        public override string GetLogMessage() => "Going up";

        public override bool IsApplicable(string instruction) => instruction.Equals(Instruction);
    }
}
