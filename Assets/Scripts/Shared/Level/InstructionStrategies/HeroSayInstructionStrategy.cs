using Assets.Scripts.Shared.Hero;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public class HeroSayInstructionStrategy : InstructionStrategy
    {
        private const string BaseInstruction = "Say";

        private readonly Speaker speaker;

        public static string GetFormattedInstruction(string message) => $"{BaseInstruction}.{message}";

        public HeroSayInstructionStrategy(GameObject hero) : base(hero)
        {
            speaker = hero.GetComponent<Speaker>();
        }

        public override async Task ExecuteInstruction(string instruction) => await speaker.SayAsync(GetMessageParameter(instruction));

        public override string GetLogMessage(string instruction) => $"Saying \"{GetMessageParameter(instruction)}\"";

        public override bool IsApplicable(string instruction) => instruction.StartsWith(BaseInstruction);

        #region Helpers
        private string GetMessageParameter(string instruction)
        {
            const int MessageParameterPosition = 1;

            var instructionParts = instruction.Split('.');

            return instructionParts[MessageParameterPosition];
        }
        #endregion
    }
}
