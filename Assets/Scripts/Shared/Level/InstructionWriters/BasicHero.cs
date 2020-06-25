using Assets.Scripts.Shared.Level.InstructionStrategies;
using System.Collections.Generic;

namespace Assets.Scripts.Shared.Level.InstructionWriters
{
    public class BasicHero
    {
        protected readonly Queue<string> instructions;

        public BasicHero(Queue<string> instructions)
        {
            this.instructions = instructions;
        }

        /// <summary>
        /// Order your hero to go down.
        /// </summary>
        public void MoveDown() => instructions.Enqueue(HeroMoveDownLevelInstructionStrategy.Instruction);

        /// <summary>
        /// Order your hero to go left.
        /// </summary>
        public void MoveLeft() => instructions.Enqueue(HeroMoveLeftLevelInstructionStrategy.Instruction);

        /// <summary>
        /// Order your hero to go right.
        /// </summary>
        public void MoveRight() => instructions.Enqueue(HeroMoveRightLevelInstructionStrategy.Instruction);

        /// <summary>
        /// Order your hero to go up.
        /// </summary>
        public void MoveUp() => instructions.Enqueue(HeroMoveUpLevelInstructionStrategy.Instruction);
    }
}
