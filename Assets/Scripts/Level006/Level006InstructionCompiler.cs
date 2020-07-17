using Assets.Scripts.Shared.Level;
using Assets.Scripts.Shared.Level.InstructionWriters;
using System.Collections.Generic;

namespace Assets.Scripts.Level006
{
    public class Level006InstructionCompiler : InstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new _Levels.Level006.Level006
            {
                Hero = new SpeakingHero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
