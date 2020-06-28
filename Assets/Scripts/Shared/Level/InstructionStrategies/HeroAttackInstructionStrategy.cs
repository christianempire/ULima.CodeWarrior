using Assets.Scripts.Shared.Hero;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public class HeroAttackInstructionStrategy : InstructionStrategy
    {
        private const string BaseInstruction = "Attack";

        private readonly Attacker attacker;
        private readonly Chaser chaser;

        public static string GetFormattedInstruction(string targetParameter) => $"{BaseInstruction}.{targetParameter}";

        public HeroAttackInstructionStrategy(GameObject hero) : base(hero)
        {
            attacker = hero.GetComponent<Attacker>();
            chaser = hero.GetComponent<Chaser>();
        }

        public override async Task ExecuteInstruction(string instruction)
        {
            var target = GetTargetParameter(instruction);

            if (!chaser.IsEntityVisible(target))
                return;

            await chaser.ChaseEntityAsync(target);
            await attacker.AttackEntityAsync(target);
        }

        public override string GetLogMessage(string instruction) => $"Attacking {GetTargetParameter(instruction)}";

        public override bool IsApplicable(string instruction) => instruction.StartsWith(BaseInstruction);

        #region Helpers
        private string GetTargetParameter(string instruction)
        {
            const int TargetParameterPosition = 1;

            var instructionParts = instruction.Split('.');

            return instructionParts[TargetParameterPosition];
        }
        #endregion
    }
}
