using Assets.Scripts.Shared.Level;
using Assets.Scripts.Shared.Level.InstructionWriters;
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
                Hero = new BasicHero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
