using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Level;
using System.IO;

namespace Assets.Scripts.Level005
{
    public class Level005DifficultyAdapter : DifficultyAdapter
    {
        public override void CountFailedExecution()
        {
            var saveObject = SaveManager.Instance.Load();

            saveObject.Level005.FailedExecutionsCount++;

            SaveManager.Instance.Save(saveObject);
        }

        public override string GetNextRecommendedLevel()
        {
            var perceivedDifficulty = GetPerceivedDifficulty();

            if (perceivedDifficulty > 5)
                return "500A";
            else
                return "006";
        }

        public override void SaveLevelVariables()
        {
            var saveObject = SaveManager.Instance.Load();

            saveObject.Level005 = GetLevelVariablesObject();

            SaveManager.Instance.Save(saveObject);
        }

        protected override int GetFailedExecutionsCount()
        {
            return SaveManager.Instance.Load().Level005.FailedExecutionsCount;
        }

        protected override string GetLevelScriptRelativePath()
        {
            return Path.Combine("_Levels", "Level005", "Level005.cs");
        }
    }
}
