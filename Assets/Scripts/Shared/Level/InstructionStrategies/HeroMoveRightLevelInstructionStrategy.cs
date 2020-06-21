using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level.CheckpointDirectionStrategies;
using System.Threading.Tasks;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public class HeroMoveRightLevelInstructionStrategy : InstructionStrategy
    {
        public const string Instruction = "HeroMoveRight";

        private readonly CheckpointSeeker checkpointSeeker;

        public HeroMoveRightLevelInstructionStrategy(CheckpointSeeker checkpointSeeker)
        {
            this.checkpointSeeker = checkpointSeeker;
        }

        public override async Task ExecuteInstruction(string instruction) => await checkpointSeeker.SeekCheckpointAsync(CheckpointDirection.Right);

        public override string GetLogMessage() => "Going right";

        public override bool IsApplicable(string instruction) => instruction.Equals(Instruction);
    }
}
