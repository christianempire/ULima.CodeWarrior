using Assets.Scripts.Level005;

namespace Assets._Levels.Level005
{
    public class Level005 : Level005Script
    {
        public override void Main()
        {
            // Defend against "Brak" and "Treg"!
            // You must attack enemies twice
            Hero.MoveRight();
            Hero.Attack("Brak");
            Hero.Attack("Brak");

        }
    }
}
