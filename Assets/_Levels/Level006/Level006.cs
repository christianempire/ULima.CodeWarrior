using Assets.Scripts.Level006;

namespace Assets._Levels.Level006
{
    public class Level006 : Level006Script
    {
        public override void Main()
        {
            // Use the "Say()" function to say the password
            // The password is: "Achoo"
            Hero.Say("Achoo");
            Hero.MoveUp(3);
        }
    }
}
