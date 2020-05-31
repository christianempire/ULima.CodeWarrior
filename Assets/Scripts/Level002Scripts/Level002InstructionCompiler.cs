using Assets._Levels.Level002;
using Assets.Scripts.Level002Scripts.InstructionWriters;
using Assets.Scripts.SharedLevelScripts;
using System.Collections.Generic;

namespace Assets.Scripts.Level002Scripts
{
    public class Level002InstructionCompiler : LevelInstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new Level002
            {
                Hero = new Hero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
