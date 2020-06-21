using Assets.Scripts.Constants;
using Assets.Scripts.Shared.Enemy;
using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level.InstructionStrategies;
using Asyncoroutine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level
{
    [RequireComponent(typeof(InstructionCompiler))]
    [RequireComponent(typeof(VictoryChecker))]
    public class LevelManager : MonoBehaviour
    {
        public GameObject Hero;

        #region Properties
        private Animator heroAnimator;
        private Queue<string> instructions;
        private KillableEnemy[] killableEnemyActors;
        private List<InstructionStrategy> levelInstructionStrategies;
        private VictoryChecker victoryChecker;
        #endregion

        async void Awake()
        {
            InitializeProperties();

            await new WaitForSeconds(1.0f);
            await ExecuteInstructionsAsync();

            if (victoryChecker.IsVictoryAchieved())
            {
                KillAllEnemies();

                heroAnimator.SetBool(HeroAnimatorConstants.IsWinningParameter, true);
                Debug.Log("Victory!");
            }
            else
            {
                heroAnimator.SetBool(HeroAnimatorConstants.IsDizzyParameter, true);
                Debug.LogError("Try again");
            }
        }

        #region Helpers
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

        private void InitializeProperties()
        {
            heroAnimator = Hero.GetComponent<Animator>();
            instructions = GetComponent<InstructionCompiler>().GetInstructions();
            killableEnemyActors = GameObject.FindObjectsOfType<KillableEnemy>();
            victoryChecker = GetComponent<VictoryChecker>();

            var heroCheckpointSeeker = Hero.GetComponent<CheckpointSeeker>();
            levelInstructionStrategies = new List<InstructionStrategy>
            {
                new HeroMoveDownLevelInstructionStrategy(heroCheckpointSeeker),
                new HeroMoveLeftLevelInstructionStrategy(heroCheckpointSeeker),
                new HeroMoveRightLevelInstructionStrategy(heroCheckpointSeeker),
                new HeroMoveUpLevelInstructionStrategy(heroCheckpointSeeker)
            };
        }

        private void KillAllEnemies()
        {
            foreach (var killableEnemyActor in killableEnemyActors)
                killableEnemyActor.Kill();
        }
        #endregion
    }
}
