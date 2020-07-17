using Assets.Scripts.Shared;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    [InitializeOnLoad]
    public class Autorun
    {
        static Autorun()
        {
            Debug.Log("Welcome to Code Warrior!");

            InitializeSaveFile();
        }

        private static void InitializeSaveFile()
        {
            if (SaveManager.Instance.DoesSaveFileExist())
                return;

            SaveManager.Instance.Save(new SaveObject());

            Debug.Log("Save file initialized!");
        }
    }
}
