using Assets.Scripts.Level003;

namespace Assets._Levels.Level003
{
    public class Level003 : Level003Script
    {
        public void Main()
        {
            // Avoid being seen by the enemy. Collect all the potions.
            Hero.MoveRight();
            Hero.MoveUp();
            Hero.MoveRight();
            Hero.MoveDown();
            Hero.MoveRight();
        }
    }
}
