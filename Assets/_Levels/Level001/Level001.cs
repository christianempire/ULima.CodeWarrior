using Assets.Scripts.Level001Scripts;

namespace Assets._Levels.Level001
{
    public class Level001 : Level001Base
    {
        public void Main()
        {
            // Move towards the apple
            // Type your code below and click Play when you're done.
            Hero.MoveRight();
            Hero.MoveDown();
            Hero.MoveRight();
        }
    }
}