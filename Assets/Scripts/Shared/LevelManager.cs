﻿using Assets.Scripts.Constants;
using Assets.Scripts.Shared.LevelInstructionStrategies;
using Asyncoroutine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Shared
{
    [RequireComponent(typeof(LevelInstructionCompiler))]
    [RequireComponent(typeof(VictoryChecker))]
    public class LevelManager : MonoBehaviour
    {
        public CheckpointSeeker CheckpointSeeker;
        public Animator HeroAnimator;

        #region Properties
        private Queue<string> instructions;
        private List<ILevelInstructionStrategy> levelInstructionStrategies;
        #endregion

        async void Start()
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
            {
                HeroAnimator.SetBool(HeroAnimatorConstants.IsWinningParameter, true);
                Debug.Log("Victory!");
            }
            else
            {
                HeroAnimator.SetBool(HeroAnimatorConstants.IsDizzyParameter, true);
                Debug.LogError("Try again");
            }
        }

        #region Helpers
        private void InitializeInstructions()
        {
            instructions = GetComponent<LevelInstructionCompiler>().GetInstructions();
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
