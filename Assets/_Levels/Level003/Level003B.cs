using Assets.Scripts.Level003;

namespace Assets._Levels.Level003
{
    public class Level003B : Level003Script
    {
        public override void Main()
        {
            // Avoid being seen by the enemies
            Hero.MoveRight();
            Hero.MoveLeft();
            Hero.MoveRight();
            Hero.MoveRight();
            Hero.MoveRight();
        }
    }
}
