﻿using Assets._Levels.Level001;
using Assets.Scripts.Level001Scripts.InstructionWritters;
using Assets.Scripts.Level001Scripts.LevelInstructionStrategies;
using Asyncoroutine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Level001Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public CheckpointSeeker CheckpointSeeker;
        public VictoryChecker VictoryChecker;

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

                if (!await levelInstructionStrategy.ExecuteInstruction(instruction))
                {
                    Debug.LogError("Try again");
                    return;
                }
            }

            if (VictoryChecker.IsVictoryAchieved())
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
