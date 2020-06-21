using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Threading.Tasks;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public class HeroMoveLeftLevelInstructionStrategy : InstructionStrategy
    {
        public const string Instruction = "HeroMoveLeft";

        private readonly CheckpointSeeker checkpointSeeker;

        public HeroMoveLeftLevelInstructionStrategy(CheckpointSeeker checkpointSeeker)
        {
            this.checkpointSeeker = checkpointSeeker;
        }

        public override async Task ExecuteInstruction(string instruction) => await checkpointSeeker.SeekCheckpointAsync(CheckpointDirection.Left);

        public override string GetLogMessage() => "Going left";

        public override bool IsApplicable(string instruction) => instruction.Equals(Instruction);
    }
}
