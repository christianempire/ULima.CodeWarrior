using Assets.Scripts.Level002.InstructionWriters;
using Assets.Scripts.Shared.Level;
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
                Hero = new Hero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
