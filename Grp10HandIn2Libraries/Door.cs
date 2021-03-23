using System;

namespace Grp10HandIn2Libraries
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArgs> DoorEvent;

        public void OpenDoor(bool doorLocked)
        {
            Console.WriteLine("Kontrollerer dør");
            OnDoorOpen(new DoorEventArgs{DoorLocked = doorLocked});
        }

        public void CloseDoor(bool doorLocked)
        {
            Console.WriteLine("Kontrollerer dør");
            OnDoorClose(new DoorEventArgs { DoorLocked = doorLocked });
        }

        //public void LockDoor()
        //{
        //    //Udskriv på display
        //    Console.WriteLine("Door has been locked");
        //}

        //public void UnlockDoor()
        //{
        //    //Udskriv på display
        //    Console.WriteLine("Door has been unlocked");
        //}

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
        public bool DoorLocked { get; set; }
    }
}
