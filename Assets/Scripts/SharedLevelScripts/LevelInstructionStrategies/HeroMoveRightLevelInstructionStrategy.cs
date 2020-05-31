using Assets.Scripts.SharedLevelScripts.SeekerDirectionStrategies;
using System.Threading.Tasks;

namespace Assets.Scripts.SharedLevelScripts.LevelInstructionStrategies
{
    public class HeroMoveRightLevelInstructionStrategy : ILevelInstructionStrategy
    {
        public const string Instruction = "HeroMoveRight";

        private readonly CheckpointSeeker checkpointSeeker;

        public HeroMoveRightLevelInstructionStrategy(CheckpointSeeker checkpointSeeker)
        {
            this.checkpointSeeker = checkpointSeeker;
        }

        public async Task ExecuteInstruction(string instruction) => await checkpointSeeker.BeginSeekingCheckpointAsync(SeekerDirection.Right);

        public string GetLogMessage() => "Going right";

        public bool IsApplicable(string instruction) => instruction.Equals(Instruction);
    }
}
