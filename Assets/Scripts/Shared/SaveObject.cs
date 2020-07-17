using System;

namespace Assets.Scripts.Shared
{
    [Serializable]
    public class SaveObject
    {
        public long LastLevelCompletionTime;
        public LevelVariablesObject Level001;
        public LevelVariablesObject Level002;
        public LevelVariablesObject Level002A;
        public LevelVariablesObject Level003;
        public LevelVariablesObject Level003A;
        public LevelVariablesObject Level003B;
        public LevelVariablesObject Level004;
        public LevelVariablesObject Level004A;
        public LevelVariablesObject Level004B;
        public LevelVariablesObject Level005;
        public LevelVariablesObject Level005A;
        public LevelVariablesObject Level005B;
        public LevelVariablesObject Level006;
        public LevelVariablesObject Level007;

        public SaveObject()
        {
            LastLevelCompletionTime = DateTime.UtcNow.Ticks;
            Level001 = new LevelVariablesObject();
            Level002 = new LevelVariablesObject();
            Level002A = new LevelVariablesObject();
            Level003 = new LevelVariablesObject();
            Level003A = new LevelVariablesObject();
            Level003B = new LevelVariablesObject();
            Level004 = new LevelVariablesObject();
            Level004A = new LevelVariablesObject();
            Level004B = new LevelVariablesObject();
            Level005 = new LevelVariablesObject();
            Level005A = new LevelVariablesObject();
            Level005B = new LevelVariablesObject();
            Level006 = new LevelVariablesObject();
            Level007 = new LevelVariablesObject();
        }
    }

    [Serializable]
    public class LevelVariablesObject
    {
        public int CodeLinesCount;
        public int FailedExecutionsCount;
        public double LevelDuration;

        public LevelVariablesObject()
        {
            CodeLinesCount = 0;
            FailedExecutionsCount = 0;
            LevelDuration = 0;
        }
    }
}
