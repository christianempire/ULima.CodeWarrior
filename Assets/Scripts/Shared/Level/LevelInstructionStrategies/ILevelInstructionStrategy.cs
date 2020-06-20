using System.Threading.Tasks;

namespace Assets.Scripts.Shared.Level.LevelInstructionStrategies
{
    public interface ILevelInstructionStrategy
    {
        Task ExecuteInstruction(string instruction);
        string GetLogMessage();
        bool IsApplicable(string instruction);
    }
}
