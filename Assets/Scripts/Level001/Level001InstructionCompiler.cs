using Assets.Scripts.Level001.InstructionWriters;
using Assets.Scripts.Shared.Level;
using System.Collections.Generic;

namespace Assets.Scripts.Level001
{
    public class Level001InstructionCompiler : InstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new _Levels.Level001.Level001
            {
                Hero = new Hero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
