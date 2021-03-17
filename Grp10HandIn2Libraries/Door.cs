using System;

namespace Grp10HandIn2Libraries
{
    public class Door : IDoor
    {
        public void LockDoor()
        {
            //Udskriv på display
            Console.WriteLine("Door has been locked");
        }

        public void UnlockDoor()
        {
            //Udskriv på display
            Console.WriteLine("Door has been unlocked");
        }

        public void OnDoorOpen()
        {
            Console.WriteLine("Door has been opened");
        }

        public void OnDoorClose()
        {
            Console.WriteLine("Door has been closed");
        }
    }
}
