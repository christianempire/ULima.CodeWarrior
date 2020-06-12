using Assets.Scripts.Shared.SeekerDirectionStrategies;
using System.Threading.Tasks;

namespace Assets.Scripts.Shared.LevelInstructionStrategies
{
    public class HeroMoveLeftLevelInstructionStrategy : ILevelInstructionStrategy
    {
        public const string Instruction = "HeroMoveLeft";

        private readonly CheckpointSeeker checkpointSeeker;

        public HeroMoveLeftLevelInstructionStrategy(CheckpointSeeker checkpointSeeker)
        {
            this.checkpointSeeker = checkpointSeeker;
        }

        public async Task ExecuteInstruction(string instruction) => await checkpointSeeker.BeginSeekingCheckpointAsync(SeekerDirection.Left);

        public string GetLogMessage() => "Going left";

        public bool IsApplicable(string instruction) => instruction.Equals(Instruction);
    }
}
