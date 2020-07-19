namespace Assets.Scripts.Shared.Level
{
    public static class LevelVariablesNormalizer
    {
        public static int GetNormalizedLevelDuration(double levelDuration)
        {
            if (levelDuration <= 30)
                return 1;
            else if (levelDuration <= 60)
                return 2;
            else if (levelDuration <= 90)
                return 3;
            else if (levelDuration <= 120)
                return 4;
            else if (levelDuration <= 150)
                return 5;
            else if (levelDuration <= 180)
                return 6;
            else if (levelDuration <= 210)
                return 7;
            else if (levelDuration <= 240)
                return 8;
            else if (levelDuration <= 270)
                return 9;
            else
                return 10;
        }

        public static int GetNormalizedCodeLinesCount(int codeLinesCount)
        {
            const int CountOffset = 13;

            var finalCount = codeLinesCount - CountOffset;

            if (finalCount <= 4)
                return 1;
            else if (finalCount <= 8)
                return 2;
            else if (finalCount <= 12)
                return 3;
            else if (finalCount <= 16)
                return 4;
            else if (finalCount <= 20)
                return 5;
            else if (finalCount <= 24)
                return 6;
            else if (finalCount <= 28)
                return 7;
            else if (finalCount <= 32)
                return 8;
            else if (finalCount <= 36)
                return 9;
            else
                return 10;
        }

        public static int GetNormalizedFailedExecutionsCount(int failedExecutionsCount)
        {
            if (failedExecutionsCount <= 10)
                return failedExecutionsCount;
            else
                return 10;
        }
    }
}
