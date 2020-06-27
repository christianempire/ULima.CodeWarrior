using Assets.Scripts.Level004;

namespace Assets._Levels.Level004
{
    public class Level004B : Level004Script
    {
        public override void Main()
        {
            // Grab the potions and go to the exit
            Hero.MoveRight(4);
            Hero.MoveDown(3);
            Hero.MoveRight(10);
            Hero.MoveUp(3);
            Hero.MoveRight(4);
        }
    }
}
