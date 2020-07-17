using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level.InstructionStrategies
{
    public abstract class InstructionStrategy
    {
        protected GameObject Hero;

        public static List<InstructionStrategy> GetStrategies(GameObject hero) => new List<InstructionStrategy>
        {
            new HeroAttackInstructionStrategy(hero),
            new HeroMoveDownInstructionStrategy(hero),
            new HeroMoveLeftInstructionStrategy(hero),
            new HeroMoveRightInstructionStrategy(hero),
            new HeroSayInstructionStrategy(hero),
            new HeroMoveUpInstructionStrategy(hero)
        };

        public InstructionStrategy(GameObject hero)
        {
            Hero = hero;
        }

        public abstract Task ExecuteInstruction(string instruction);

        public abstract string GetLogMessage(string instruction);

        public abstract bool IsApplicable(string instruction);
    }
}
