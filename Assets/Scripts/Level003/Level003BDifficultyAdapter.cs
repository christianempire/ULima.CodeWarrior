using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Level;
using System.IO;

namespace Assets.Scripts.Level003
{
    public class Level003BDifficultyAdapter : DifficultyAdapter
    {
        public override void CountFailedExecution()
        {
            var saveObject = SaveManager.Instance.Load();

            saveObject.Level003B.FailedExecutionsCount++;

            SaveManager.Instance.Save(saveObject);
        }

        public override string GetNextRecommendedLevel()
        {
            return "004";
        }

        public override void SaveLevelVariables()
        {
            var saveObject = SaveManager.Instance.Load();

            saveObject.Level003B = GetLevelVariablesObject();

            SaveManager.Instance.Save(saveObject);
        }

        protected override int GetFailedExecutionsCount()
        {
            return SaveManager.Instance.Load().Level003B.FailedExecutionsCount;
        }

        protected override string GetLevelScriptRelativePath()
        {
            return Path.Combine("_Levels", "Level003", "Level003B.cs");
        }
    }
}
