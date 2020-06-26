using Assets.Scripts.Level004;

namespace Assets._Levels.Level004
{
    public class Level004A : Level004Script
    {
        public override void Main()
        {
            // Use the Illusion Ring to create an Illusion that will distract the enemies
            Hero.MoveRight();
            Hero.MoveDown(2);
            Hero.MoveUp(2);
            Hero.MoveRight(4);
        }
    }
}
