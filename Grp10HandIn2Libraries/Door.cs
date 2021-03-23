using System;

namespace Grp10HandIn2Libraries
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArgs> DoorOpenEvent;
        public event EventHandler<DoorEventArgs> DoorCloseEvent;

        public void OpenCloseDoor(bool openDoor)
        {
            Console.WriteLine("Kontrollerer dør");

            if (openDoor)
            {
                OnDoorOpen(new DoorEventArgs { OpenDoor = openDoor });
            }
            else
            {
                OnDoorClose(new DoorEventArgs { OpenDoor = openDoor });
            }

                
        }

        public void CloseDoor(bool openDoor)
        {

        }
        public void DoOpenDoor()
        {
            //Udskriv på display
            Console.WriteLine("Door has been opened");

        }


        public void DoCloseDoor()
        {
            //Udskriv på display
            Console.WriteLine("Door has been closed");

        }




        //public void CloseDoor()
        //{
        //    Console.WriteLine("Kontrollerer dør");
        //    OnDoorClose(new DoorEventArgs {});
        //}

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
            DoorOpenEvent?.Invoke(this, e);
        }

        protected virtual void OnDoorClose(DoorEventArgs e)
        {
            DoorCloseEvent?.Invoke(this, e);
        }
    }

    public class DoorEventArgs : EventArgs
    {
        public bool OpenDoor { get; set; }
    }
}
