﻿using Assets.Scripts.Level002;

namespace Assets._Levels.Level002
{
    public class Level002 : Level002Script
    {
        public void Main()
        {
            // Grab all the potions using your movement commands.
            Hero.MoveRight();
            Hero.MoveDown();
            Hero.MoveUp();
            Hero.MoveUp();
            Hero.MoveRight();
        }
    }
}
