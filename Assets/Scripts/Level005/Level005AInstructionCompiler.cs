using Assets.Scripts.Shared.Level;
using Assets.Scripts.Shared.Level.InstructionWriters;
using System.Collections.Generic;

namespace Assets.Scripts.Level005
{
    public class Level005AInstructionCompiler : InstructionCompiler
    {
        public override Queue<string> GetInstructions()
        {
            Queue<string> instructions = new Queue<string>();
            var level = new _Levels.Level005.Level005A
            {
                Hero = new AttackingHero(instructions)
            };

            level.Main();

            return instructions;
        }
    }
}
