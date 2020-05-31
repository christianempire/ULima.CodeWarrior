using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SharedLevelScripts
{
    public abstract class LevelInstructionCompiler : MonoBehaviour
    {
        public abstract Queue<string> GetInstructions();
    }
}
