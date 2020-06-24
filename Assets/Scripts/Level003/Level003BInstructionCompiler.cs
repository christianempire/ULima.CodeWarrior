using Assets.Scripts.Level003.InstructionWriters;
using Assets.Scripts.Shared.Level;
using System.Collections.Generic;

namespace Assets.Scripts.Level003
{
    public class Level003BInstructionCompiler : InstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new _Levels.Level003.Level003B
            {
                Hero = new Hero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
