using Assets.Scripts.Constants.Hero;
using Assets.Scripts.Shared.Enemy;
using Assets.Scripts.Shared.Level.InstructionStrategies;
using Asyncoroutine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shared.Level
{
    [RequireComponent(typeof(DifficultyAdapter))]
    [RequireComponent(typeof(InstructionCompiler))]
    [RequireComponent(typeof(VictoryChecker))]
    public class LevelManager : MonoBehaviour
    {
        public GameObject Hero;

        #region Properties
        private DifficultyAdapter difficultyAdapter;
        private Animator heroAnimator;
        private Queue<string> instructions;
        private KillableEnemy[] killableEnemyActors;
        private VictoryChecker victoryChecker;
        #endregion

        async void Awake()
        {
            InitializeProperties();

            await new WaitForSeconds(1.0f);
            await ExecuteInstructionsAsync();

            if (await victoryChecker.IsVictoryAchievedAsync())
            {
                KillAllEnemies();
                UpdateSaveFile();

                difficultyAdapter.SaveLevelVariables();

                heroAnimator.SetBool(HeroAnimatorConstants.IsWinningParameter, true);
                Debug.Log("Victory!");
                Debug.Log($"Proceed to Level {difficultyAdapter.GetNextRecommendedLevel()}");
            }
            else
            {
                heroAnimator.SetBool(HeroAnimatorConstants.IsDizzyParameter, true);
                Debug.LogError("Try again");

                difficultyAdapter.CountFailedExecution();
            }
        }

        #region Helpers
        private async Task ExecuteInstructionsAsync()
        {
            while (instructions.Count > 0)
            {
                var instruction = instructions.Dequeue();
                var instructionStrategy = InstructionStrategy.GetStrategies(Hero)
                    .First(strategy => strategy.IsApplicable(instruction));

                Debug.Log(instructionStrategy.GetLogMessage(instruction));

                await instructionStrategy.ExecuteInstruction(instruction);
            }
        }

        private void InitializeProperties()
        {
            difficultyAdapter = GetComponent<DifficultyAdapter>();
            heroAnimator = Hero.GetComponent<Animator>();
            instructions = GetComponent<InstructionCompiler>().GetInstructions();
            killableEnemyActors = FindObjectsOfType<KillableEnemy>();
            victoryChecker = GetComponent<VictoryChecker>();
        }

        private void KillAllEnemies()
        {
            foreach (var killableEnemyActor in killableEnemyActors)
                killableEnemyActor.Kill();
        }

        private void UpdateSaveFile()
        {
            var saveObject = SaveManager.Instance.Load();

            saveObject.LastLevelCompletionTime = DateTime.UtcNow.Ticks;

            SaveManager.Instance.Save(saveObject);
        }
        #endregion
    }
}
