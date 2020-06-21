using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Threading.Tasks;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public class HeroMoveDownLevelInstructionStrategy : InstructionStrategy
    {
        public const string Instruction = "HeroMoveDown";

        private readonly CheckpointSeeker checkpointSeeker;

        public HeroMoveDownLevelInstructionStrategy(CheckpointSeeker checkpointSeeker)
        {
            this.checkpointSeeker = checkpointSeeker;
        }

        public override async Task ExecuteInstruction(string instruction) => await checkpointSeeker.SeekCheckpointAsync(CheckpointDirection.Down);

        public override string GetLogMessage() => "Going down";

        public override bool IsApplicable(string instruction) => instruction.Equals(Instruction);
    }
}
