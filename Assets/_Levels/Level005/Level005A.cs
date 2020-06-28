using Assets.Scripts.Level005;

namespace Assets._Levels.Level005
{
    public class Level005A : Level005Script
    {
        public override void Main()
        {
            // Attack both enemies and grab the potion
            Hero.MoveRight(2);
            Hero.Attack("Krug");
            Hero.Attack("Krug");
            Hero.MoveRight(2);
            Hero.MoveUp(2);
            Hero.Attack("Grump");
            Hero.Attack("Grump");
            Hero.MoveLeft(4);
        }
    }
}
