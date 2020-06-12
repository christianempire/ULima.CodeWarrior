using Assets.Scripts.Constants;
using Assets.Scripts.Shared.LevelInstructionStrategies;
using Asyncoroutine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared
{
    [RequireComponent(typeof(LevelInstructionCompiler))]
    [RequireComponent(typeof(VictoryChecker))]
    public class LevelManager : MonoBehaviour
    {
        public GameObject Hero;

        #region Properties
        private Queue<string> instructions;
        private List<ILevelInstructionStrategy> levelInstructionStrategies;
        #endregion

        async void Start()
        {
            InitializeInstructions();
            InitializeLevelInstructionStrategies();

            await new WaitForSeconds(1.0f);
            await ExecuteInstructionsAsync();

            CheckVictory();
        }

        #region Helpers
        private void CheckVictory()
        {
            var heroAnimator = Hero.GetComponent<Animator>();

            if (GetComponent<VictoryChecker>().IsVictoryAchieved())
            {
                heroAnimator.SetBool(HeroAnimatorConstants.IsWinningParameter, true);
                Debug.Log("Victory!");
            }
            else
            {
                heroAnimator.SetBool(HeroAnimatorConstants.IsDizzyParameter, true);
                Debug.LogError("Try again");
            }
        }

        private async Task ExecuteInstructionsAsync()
        {
            while (instructions.Count > 0)
            {
                var instruction = instructions.Dequeue();
                var levelInstructionStrategy = levelInstructionStrategies
                    .First(strategy => strategy.IsApplicable(instruction));

                Debug.Log(levelInstructionStrategy.GetLogMessage());

                await levelInstructionStrategy.ExecuteInstruction(instruction);
            }
        }

        private void InitializeInstructions()
        {
            instructions = GetComponent<LevelInstructionCompiler>().GetInstructions();
        }

        private void InitializeLevelInstructionStrategies()
        {
            var heroCheckpointSeeker = Hero.GetComponent<CheckpointSeeker>();

            levelInstructionStrategies = new List<ILevelInstructionStrategy>
            {
                new HeroMoveDownLevelInstructionStrategy(heroCheckpointSeeker),
                new HeroMoveLeftLevelInstructionStrategy(heroCheckpointSeeker),
                new HeroMoveRightLevelInstructionStrategy(heroCheckpointSeeker),
                new HeroMoveUpLevelInstructionStrategy(heroCheckpointSeeker)
            };
        }
        #endregion
    }
}
