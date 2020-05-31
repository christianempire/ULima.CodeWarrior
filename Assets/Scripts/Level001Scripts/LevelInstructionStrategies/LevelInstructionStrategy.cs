using System.Threading.Tasks;

namespace Assets.Scripts.Level001Scripts.LevelInstructionStrategies
{
    public interface ILevelInstructionStrategy
    {
        Task<bool> ExecuteInstruction(string instruction);
        string GetLogMessage();
        bool IsApplicable(string instruction);
    }
}
