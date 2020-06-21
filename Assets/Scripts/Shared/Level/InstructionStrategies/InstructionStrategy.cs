using System.Threading.Tasks;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public abstract class InstructionStrategy
    {
        public abstract Task ExecuteInstruction(string instruction);

        public abstract string GetLogMessage();

        public abstract bool IsApplicable(string instruction);
    }
}
