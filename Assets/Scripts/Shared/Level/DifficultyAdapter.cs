using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Shared.Level
{
    public abstract class DifficultyAdapter : MonoBehaviour
    {
        public abstract void CountFailedExecution();

        public abstract string GetNextRecommendedLevel();

        public abstract void SaveLevelVariables();

        protected abstract int GetFailedExecutionsCount();

        protected abstract string GetLevelScriptRelativePath();

        protected int GetPerceivedDifficulty()
        {
            var levelVariablesObject = GetLevelVariablesObject();
            var normalizedLevelDuration = LevelVariablesNormalizer.GetNormalizedLevelDuration(levelVariablesObject.LevelDuration);
            var normalizedCodeLinesCount = LevelVariablesNormalizer.GetNormalizedCodeLinesCount(levelVariablesObject.CodeLinesCount);
            var normalizedFailedExecutionsCount = LevelVariablesNormalizer.GetNormalizedFailedExecutionsCount(levelVariablesObject.FailedExecutionsCount);

            return Mathf.RoundToInt((normalizedLevelDuration + normalizedCodeLinesCount + normalizedFailedExecutionsCount) / 3.0f);
        }

        protected LevelVariablesObject GetLevelVariablesObject()
        {
            return new LevelVariablesObject
            {
                LevelDuration = GetLevelDuration(),
                CodeLinesCount = GetCodeLinesCount(),
                FailedExecutionsCount = GetFailedExecutionsCount()
            };
        }

        #region Helpers
        private int GetCodeLinesCount()
        {
            var levelScriptFilePath = Path.Combine(Application.dataPath, GetLevelScriptRelativePath());
            var codeLinesCount = 0;

            using (var streamReader = new StreamReader(levelScriptFilePath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                    codeLinesCount++;
            }

            return codeLinesCount;
        }

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        private double GetLevelDuration()
        {
            var levelDurationTimeSpan = TimeSpan.FromTicks(DateTime.UtcNow.Ticks - SaveManager.Instance.Load().LastLevelCompletionTime);

            return levelDurationTimeSpan.TotalSeconds;
        }
        #endregion
    }
}
