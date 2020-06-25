using Assets.Scripts.Shared.Level;
using Assets.Scripts.Shared.Level.InstructionWriters;
using System.Collections.Generic;

namespace Assets.Scripts.Level003
{
    public class Level003AInstructionCompiler : InstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new _Levels.Level003.Level003A
            {
                Hero = new BasicHero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
