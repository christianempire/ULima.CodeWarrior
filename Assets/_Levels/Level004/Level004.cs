using Assets.Scripts.Level004;

namespace Assets._Levels.Level004
{
    public class Level004 : Level004Script
    {
        public override void Main()
        {
            // Use arguments with move statements to move faster
            Hero.MoveRight(4);
            Hero.MoveUp();
            Hero.MoveRight(2);
            Hero.MoveDown(3);
            Hero.MoveRight(3);
        }
    }
}
