using Assets.Scripts.Shared.Level.InstructionStrategies;
using System.Collections.Generic;

namespace Assets.Scripts.Shared.Level.InstructionWriters
{
    public class SpeakingHero : AttackingHero
    {
        public SpeakingHero(Queue<string> instructions) : base(instructions)
        {
        }

        /// <summary>
        /// Order your hero to say something.
        /// </summary>
        /// <param name="message">The message to say.</param>
        public void Say(string message) => instructions.Enqueue(HeroSayInstructionStrategy.GetFormattedInstruction(message));
    }
}
