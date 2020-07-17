using Assets.Scripts.Shared.Level.InstructionStrategies;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Shared.Level.InstructionWriters
{
    public class MovingHero : BasicHero
    {
        public MovingHero(Queue<string> instructions) : base(instructions)
        {
        }

        /// <summary>
        /// Order your hero to go down a certain number of times.
        /// </summary>
        /// <param name="movements">How many times should your hero move.</param>
        public void MoveDown(int movements)
        {
            if (movements <= 0)
                throw new ArgumentException("Invalid number of movements!");

            for (int i = 0; i < movements; i++)
                instructions.Enqueue(HeroMoveDownInstructionStrategy.GetFormattedInstruction());
        }

        /// <summary>
        /// Order your hero to go left a certain number of times.
        /// </summary>
        /// <param name="movements">How many times should your hero move.</param>
        public void MoveLeft(int movements)
        {
            if (movements <= 0)
                throw new ArgumentException("Invalid number of movements!");

            for (int i = 0; i < movements; i++)
                instructions.Enqueue(HeroMoveLeftInstructionStrategy.GetFormattedInstruction());
        }

        /// <summary>
        /// Order your hero to go right a certain number of times.
        /// </summary>
        /// <param name="movements">How many times should your hero move.</param>
        public void MoveRight(int movements)
        {
            if (movements <= 0)
                throw new ArgumentException("Invalid number of movements!");

            for (int i = 0; i < movements; i++)
                instructions.Enqueue(HeroMoveRightInstructionStrategy.GetFormattedInstruction());
        }

        /// <summary>
        /// Order your hero to go up a certain number of times.
        /// </summary>
        /// <param name="movements">How many times should your hero move.</param>
        public void MoveUp(int movements)
        {
            if (movements <= 0)
                throw new ArgumentException("Invalid number of movements!");

            for (int i = 0; i < movements; i++)
                instructions.Enqueue(HeroMoveUpInstructionStrategy.GetFormattedInstruction());
        }
    }
}
