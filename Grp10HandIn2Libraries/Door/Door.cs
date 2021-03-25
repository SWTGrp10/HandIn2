using System;

namespace Grp10HandIn2Libraries
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArgs> DoorChangedEvent;

        public string DoorString { get; private set; }

        public void OpenCloseDoor(bool openDoor)
        {
            OnDoorChanged(new DoorEventArgs { OpenDoor = openDoor });
        }

        public void LockDoor()
        {
            //Udskriv på display
            DoorString = "Door has been locked";
            Console.WriteLine(DoorString);
        }

        public void UnlockDoor()
        {
            //Udskriv på display
            DoorString = "Door has been unlocked";
            Console.WriteLine(DoorString);
        }

        protected virtual void OnDoorChanged(DoorEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }

    }
}
