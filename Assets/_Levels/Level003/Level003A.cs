using Assets.Scripts.Level003;

namespace Assets._Levels.Level003
{
    public class Level003A : Level003Script
    {
        public override void Main()
        {
            // Avoid the enemies and grab the potion.
            Hero.MoveDown();
            Hero.MoveDown();
            Hero.MoveRight();
            Hero.MoveUp();
            Hero.MoveRight();
        }
    }
}
