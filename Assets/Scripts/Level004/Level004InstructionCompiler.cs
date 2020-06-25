﻿using Assets.Scripts.Shared.Level;
using Assets.Scripts.Shared.Level.InstructionWriters;
using System.Collections.Generic;

namespace Assets.Scripts.Level004
{
    public class Level004InstructionCompiler : InstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new _Levels.Level004.Level004
            {
                Hero = new MovingHero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
