﻿using Assets.Scripts.Shared.Level;
using Assets.Scripts.Shared.Level.InstructionWriters;
using System.Collections.Generic;

namespace Assets.Scripts.Level002
{
    public class Level002AInstructionCompiler : InstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new _Levels.Level002.Level002A
            {
                Hero = new BasicHero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
