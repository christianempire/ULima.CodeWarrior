using Assets.Scripts.Level002Scripts;

namespace Assets._Levels.Level002
{
    public class Level002 : Level002Base
    {
        public void Main()
        {
            // Begin writing your code here!
            Hero.MoveRight();
            Hero.MoveDown();
            Hero.MoveUp();
            Hero.MoveUp();
            Hero.MoveRight();
        }
    }
}
