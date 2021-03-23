using System;

namespace Grp10HandIn2Libraries
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArgs> DoorEvent;

        public void OpenDoor(bool openDoor)
        {
            Console.WriteLine("Kontrollerer dør");
            OnDoorOpen(new DoorEventArgs{OpenDoor = openDoor});
        }

        public void CloseDoor(bool openDoor)
        {
            Console.WriteLine("Kontrollerer dør");
            OnDoorClose(new DoorEventArgs { OpenDoor = openDoor });
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

        protected virtual void OnDoorOpen(DoorEventArgs e)
        {
            Console.WriteLine("Door has been opened");
            DoorEvent?.Invoke(this, e);
        }

        protected virtual void OnDoorClose(DoorEventArgs e)
        {
            Console.WriteLine("Door has been closed");
            DoorEvent?.Invoke(this, e);
        }
    }

    public class DoorEventArgs : EventArgs
    {
        public bool OpenDoor { get; set; }
    }
}
