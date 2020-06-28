using Assets.Scripts.Shared.Level.InstructionStrategies;
using System.Collections.Generic;

namespace Assets.Scripts.Shared.Level.InstructionWriters
{
    public class AttackingHero : MovingHero
    {
        public AttackingHero(Queue<string> instructions) : base(instructions)
        {
        }

        /// <summary>
        /// Order your hero to attack a target.
        /// </summary>
        /// <param name="target">The target's name.</param>
        public void Attack(string target) => instructions.Enqueue(HeroAttackInstructionStrategy.GetFormattedInstruction(target));
    }
}
