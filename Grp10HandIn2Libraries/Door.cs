using System;

namespace Grp10HandIn2Libraries
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArgs> DoorChangedEvent;


        public void OpenCloseDoor(bool openDoor)
        {
            OnDoorChanged(new DoorEventArgs { OpenDoor = openDoor });
            
        }


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


        protected virtual void OnDoorChanged(DoorEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }

    }
}
