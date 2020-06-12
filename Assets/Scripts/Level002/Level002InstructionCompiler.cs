﻿using Assets._Levels.Level102;
using Assets.Scripts.Level002.InstructionWriters;
using Assets.Scripts.Shared;
using System.Collections.Generic;

namespace Assets.Scripts.Level002
{
    public class Level002InstructionCompiler : LevelInstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new Level102
            {
                Hero = new Hero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}