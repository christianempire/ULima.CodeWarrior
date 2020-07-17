using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Level;
using System.IO;

namespace Assets.Scripts.Level003
{
    public class Level003ADifficultyAdapter : DifficultyAdapter
    {
        public override void CountFailedExecution()
        {
            var saveObject = SaveManager.Instance.Load();

            saveObject.Level003A.FailedExecutionsCount++;

            SaveManager.Instance.Save(saveObject);
        }

        public override string GetNextRecommendedLevel()
        {
            var perceivedDifficulty = GetPerceivedDifficulty();

            if (perceivedDifficulty > 5)
                return "300B";
            else
                return "004";
        }

        public override void SaveLevelVariables()
        {
            var saveObject = SaveManager.Instance.Load();

            saveObject.Level003A = GetLevelVariablesObject();

            SaveManager.Instance.Save(saveObject);
        }

        protected override int GetFailedExecutionsCount()
        {
            return SaveManager.Instance.Load().Level003A.FailedExecutionsCount;
        }

        protected override string GetLevelScriptRelativePath()
        {
            return Path.Combine("_Levels", "Level003", "Level003A.cs");
        }
    }
}
