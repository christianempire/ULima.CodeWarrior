using Assets.Scripts.Level001Scripts.SeekerDirectionStrategies;
using System.Threading.Tasks;

namespace Assets.Scripts.Level001Scripts.LevelInstructionStrategies
{
    public class HeroMoveDownLevelInstructionStrategy : ILevelInstructionStrategy
    {
        public const string Instruction = "HeroMoveDown";

        private readonly CheckpointSeeker checkpointSeeker;

        public HeroMoveDownLevelInstructionStrategy(CheckpointSeeker checkpointSeeker)
        {
            this.checkpointSeeker = checkpointSeeker;
        }

        public async Task<bool> ExecuteInstruction(string instruction) => await checkpointSeeker.BeginSeekingCheckpointAsync(SeekerDirection.Down);

        public string GetLogMessage() => "Going down";

        public bool IsApplicable(string instruction) => instruction.Equals(Instruction);
    }
}
