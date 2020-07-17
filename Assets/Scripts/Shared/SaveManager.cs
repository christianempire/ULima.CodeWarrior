using System.IO;
using UnityEngine;

namespace Assets.Scripts.Shared
{
    public class SaveManager
    {
        #region Singleton
        private static SaveManager instance;
        public static SaveManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SaveManager();

                return instance;
            }
        }
        #endregion

        private readonly string saveFilePath;

        private SaveManager()
        {
            saveFilePath = Path.Combine(Application.persistentDataPath, "UserData.json");
        }

        public bool DoesSaveFileExist() => File.Exists(saveFilePath);

        public SaveObject Load()
        {
            var serializedSaveObject = string.Empty;

            using (var streamReader = new StreamReader(saveFilePath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                    serializedSaveObject += line;
            }

            return JsonUtility.FromJson<SaveObject>(serializedSaveObject);
        }

        public bool Save(SaveObject saveObject)
        {
            var serializedSaveObject = JsonUtility.ToJson(saveObject);

            using (var streamWriter = new StreamWriter(saveFilePath))
            {
                streamWriter.WriteLine(serializedSaveObject);
            }

            return true;
        }
    }
}
