using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Hero.SeekerDirectionStrategies;
using System.Threading.Tasks;

namespace Assets.Scripts.Shared.Level.LevelInstructionStrategies
{
    public class HeroMoveDownLevelInstructionStrategy : ILevelInstructionStrategy
    {
        public const string Instruction = "HeroMoveDown";

        private readonly CheckpointSeeker checkpointSeeker;

        public HeroMoveDownLevelInstructionStrategy(CheckpointSeeker checkpointSeeker)
        {
            this.checkpointSeeker = checkpointSeeker;
        }

        public async Task ExecuteInstruction(string instruction) => await checkpointSeeker.SeekCheckpointAsync(SeekerDirection.Down);

        public string GetLogMessage() => "Going down";

        public bool IsApplicable(string instruction) => instruction.Equals(Instruction);
    }
}
