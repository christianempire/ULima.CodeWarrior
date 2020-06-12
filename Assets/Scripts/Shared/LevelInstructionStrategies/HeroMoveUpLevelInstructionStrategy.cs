using Assets.Scripts.Shared.SeekerDirectionStrategies;
using System.Threading.Tasks;

namespace Assets.Scripts.Shared.LevelInstructionStrategies
{
    public class HeroMoveUpLevelInstructionStrategy : ILevelInstructionStrategy
    {
        public const string Instruction = "HeroMoveUp";

        private readonly CheckpointSeeker checkpointSeeker;

        public HeroMoveUpLevelInstructionStrategy(CheckpointSeeker checkpointSeeker)
        {
            this.checkpointSeeker = checkpointSeeker;
        }

        public async Task ExecuteInstruction(string instruction) => await checkpointSeeker.BeginSeekingCheckpointAsync(SeekerDirection.Up);

        public string GetLogMessage() => "Going up";

        public bool IsApplicable(string instruction) => instruction.Equals(Instruction);
    }
}
