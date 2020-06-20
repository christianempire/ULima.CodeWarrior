using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Hero.SeekerDirectionStrategies;
using System.Threading.Tasks;

namespace Assets.Scripts.Shared.Level.LevelInstructionStrategies
{
    public class HeroMoveRightLevelInstructionStrategy : ILevelInstructionStrategy
    {
        public const string Instruction = "HeroMoveRight";

        private readonly CheckpointSeeker checkpointSeeker;

        public HeroMoveRightLevelInstructionStrategy(CheckpointSeeker checkpointSeeker)
        {
            this.checkpointSeeker = checkpointSeeker;
        }

        public async Task ExecuteInstruction(string instruction) => await checkpointSeeker.SeekCheckpointAsync(SeekerDirection.Right);

        public string GetLogMessage() => "Going right";

        public bool IsApplicable(string instruction) => instruction.Equals(Instruction);
    }
}
