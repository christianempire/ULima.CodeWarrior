using Assets.Scripts.Level005;
using System;

namespace Assets._Levels.Level005
{
    public class Level005B : Level005Script
    {
        public override void Main()
        {
            // Defeat the enemies.
            // Remember that they each take two hits.
            Hero.Attack("Gurt");
            Hero.Attack("Gurt");
            Hero.Attack("Rig");
            Hero.Attack("Rig");
            Hero.Attack("Ack");
            Hero.Attack("Ack");
        }
    }
}
