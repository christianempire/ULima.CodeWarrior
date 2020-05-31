using Assets._Levels.Level001;
using Assets.Scripts.Level001Scripts.InstructionWriters;
using Assets.Scripts.SharedLevelScripts;
using System.Collections.Generic;

namespace Assets.Scripts.Level001Scripts
{
    public class Level001InstructionCompiler : LevelInstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new Level001
            {
                Hero = new Hero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
