using Assets._Levels.Level001;
using Assets.Scripts.Level001Scripts.InstructionWriters;
using Assets.Scripts.Level001Scripts.LevelInstructionStrategies;
using Asyncoroutine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Level001Scripts
{
    [RequireComponent(typeof(VictoryChecker))]
    public class LevelManager : MonoBehaviour
    {
        public CheckpointSeeker CheckpointSeeker;

        #region Properties
        private Queue<string> instructions;
        private List<ILevelInstructionStrategy> levelInstructionStrategies;
        #endregion

        async void Awake()
        {
            InitializeInstructions();
            InitializeLevelInstructionStrategies();

            await new WaitForSeconds(1.0f);

            while (instructions.Count > 0)
            {
                var instruction = instructions.Dequeue();
                var levelInstructionStrategy = levelInstructionStrategies
                    .First(strategy => strategy.IsApplicable(instruction));

                Debug.Log(levelInstructionStrategy.GetLogMessage());

                await levelInstructionStrategy.ExecuteInstruction(instruction);
            }

            if (GetComponent<VictoryChecker>().IsVictoryAchieved())
                Debug.Log("Victory!");
            else
                Debug.LogError("Try again");
        }

        #region Helpers
        private void InitializeInstructions()
        {
            instructions = new Queue<string>();
            var level = new Level001
            {
                Hero = new Hero(instructions)
            };

            level.Main();
        }

        private void InitializeLevelInstructionStrategies()
        {
            levelInstructionStrategies = new List<ILevelInstructionStrategy>
            {
                new HeroMoveDownLevelInstructionStrategy(CheckpointSeeker),
                new HeroMoveLeftLevelInstructionStrategy(CheckpointSeeker),
                new HeroMoveRightLevelInstructionStrategy(CheckpointSeeker),
                new HeroMoveUpLevelInstructionStrategy(CheckpointSeeker)
            };
        }
        #endregion
    }
}
