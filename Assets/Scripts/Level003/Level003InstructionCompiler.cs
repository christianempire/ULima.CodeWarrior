using Assets.Scripts.Level003.InstructionWriters;
using Assets.Scripts.Shared.Level;
using System.Collections.Generic;

namespace Assets.Scripts.Level003
{
    public class Level003InstructionCompiler : InstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new _Levels.Level003.Level003
            {
                Hero = new Hero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
