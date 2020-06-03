using Assets._Levels.Level003;
using Assets.Scripts.Level003Scripts.InstructionWriters;
using Assets.Scripts.SharedLevelScripts;
using System.Collections.Generic;

namespace Assets.Scripts.Level003Scripts
{
    public class Level003InstructionCompiler : LevelInstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new Level003
            {
                Hero = new Hero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
