using Assets.Scripts.Level002;

namespace Assets._Levels.Level102
{
    public class Level102 : Level002Base
    {
        public void Main()
        {
            // Grab all the apples using your movement commands.
            Hero.MoveRight();
            Hero.MoveDown();
            Hero.MoveLeft();
            Hero.MoveRight();
            Hero.MoveUp();
            Hero.MoveUp();
            Hero.MoveLeft();
            Hero.MoveRight();
            Hero.MoveRight();
        }
    }
}
