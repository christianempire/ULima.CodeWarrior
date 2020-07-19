using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Level;
using System.IO;

namespace Assets.Scripts.Level004
{
    public class Level004ADifficultyAdapter : DifficultyAdapter
    {
        public override void CountFailedExecution()
        {
            var saveObject = SaveManager.Instance.Load();

            saveObject.Level004A.FailedExecutionsCount++;

            SaveManager.Instance.Save(saveObject);
        }

        public override string GetNextRecommendedLevel()
        {
            var perceivedDifficulty = GetPerceivedDifficulty();

            if (perceivedDifficulty <= 3)
                return "005";
            else
                return "004B";
        }

        public override void SaveLevelVariables()
        {
            var saveObject = SaveManager.Instance.Load();

            saveObject.Level004A = GetLevelVariablesObject();

            SaveManager.Instance.Save(saveObject);
        }

        protected override int GetFailedExecutionsCount()
        {
            return SaveManager.Instance.Load().Level004A.FailedExecutionsCount;
        }

        protected override string GetLevelScriptRelativePath()
        {
            return Path.Combine("_Levels", "Level004", "Level004A.cs");
        }
    }
}
