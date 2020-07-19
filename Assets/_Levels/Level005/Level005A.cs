using Assets.Scripts.Level005;

namespace Assets._Levels.Level005
{
    public class Level005A : Level005Script
    {
        public override void Main()
        {
            // Attack both enemies and grab the potion
            Hero.MoveRight();
            Hero.MoveRight();
            Hero.Attack("Krug");
            Hero.Attack("Krug");

        }
    }
}
