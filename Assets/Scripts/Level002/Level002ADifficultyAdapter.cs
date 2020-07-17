using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Level;
using System.IO;

namespace Assets.Scripts.Level002
{
    public class Level002ADifficultyAdapter : DifficultyAdapter
    {
        public override void CountFailedExecution()
        {
            var saveObject = SaveManager.Instance.Load();

            saveObject.Level002A.FailedExecutionsCount++;

            SaveManager.Instance.Save(saveObject);
        }

        public override string GetNextRecommendedLevel()
        {
            return "003";
        }

        public override void SaveLevelVariables()
        {
            var saveObject = SaveManager.Instance.Load();

            saveObject.Level002A = GetLevelVariablesObject();

            SaveManager.Instance.Save(saveObject);
        }

        protected override int GetFailedExecutionsCount()
        {
            return SaveManager.Instance.Load().Level002A.FailedExecutionsCount;
        }

        protected override string GetLevelScriptRelativePath()
        {
            return Path.Combine("_Levels", "Level002", "Level002A.cs");
        }
    }
}
