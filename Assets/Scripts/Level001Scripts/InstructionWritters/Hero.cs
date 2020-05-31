using Assets.Scripts.Level001Scripts.LevelInstructionStrategies;
using System.Collections.Generic;

namespace Assets.Scripts.Level001Scripts.InstructionWritters
{
    public class Hero
    {
        private readonly Queue<string> instructions;

        public Hero(Queue<string> instructions)
        {
            this.instructions = instructions;
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveDown() => instructions.Enqueue(HeroMoveDownLevelInstructionStrategy.Instruction);

        /// <summary>
        /// 
        /// </summary>
        public void MoveLeft() => instructions.Enqueue(HeroMoveLeftLevelInstructionStrategy.Instruction);

        /// <summary>
        /// 
        /// </summary>
        public void MoveRight() => instructions.Enqueue(HeroMoveRightLevelInstructionStrategy.Instruction);

        /// <summary>
        /// 
        /// </summary>
        public void MoveUp() => instructions.Enqueue(HeroMoveUpLevelInstructionStrategy.Instruction);
    }
}
