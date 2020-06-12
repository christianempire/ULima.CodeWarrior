using Assets.Scripts.Level002;

namespace Assets._Levels.Level002
{
    public class Level002 : Level002Base
    {
        public void Main()
        {
            // Grab all the potions using your movement commands.
            Hero.MoveRight();
            Hero.MoveDown();
            Hero.MoveUp();
            Hero.MoveRight();
            Hero.MoveLeft();
            Hero.MoveUp();
            Hero.MoveLeft();
            Hero.MoveRight();
            Hero.MoveRight();
        }
    }
}
