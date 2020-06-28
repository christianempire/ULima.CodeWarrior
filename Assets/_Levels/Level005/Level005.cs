using Assets.Scripts.Level005;

namespace Assets._Levels.Level005
{
    public class Level005 : Level005Script
    {
        public override void Main()
        {
            // Defend against "Brak" and "Treg"!
            // You must attack enemies twice
            Hero.MoveRight(2);
            Hero.Attack("Brak");
            Hero.Attack("Brak");
            Hero.MoveRight(4);
            Hero.Attack("Treg");
            Hero.Attack("Treg");
            Hero.MoveRight(4);
        }
    }
}
