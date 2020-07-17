using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shared.Level
{
    public abstract class InstructionCompiler : MonoBehaviour
    {
        public abstract Queue<string> GetInstructions();
    }
}
