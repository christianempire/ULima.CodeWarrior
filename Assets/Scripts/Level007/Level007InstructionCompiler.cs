using Assets.Scripts.Shared.Level;
using Assets.Scripts.Shared.Level.InstructionWriters;
using System.Collections.Generic;

namespace Assets.Scripts.Level007
{
    public class Level007InstructionCompiler : InstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new _Levels.Level007.Level007
            {
                Hero = new SpeakingHero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
