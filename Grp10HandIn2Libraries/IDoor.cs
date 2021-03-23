using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public interface IDoor
    {
        event EventHandler<DoorEventArgs> DoorOpenEvent;
        event EventHandler<DoorEventArgs> DoorCloseEvent;
        void LockDoor();

        void UnlockDoor();

        void DoOpenDoor();
        void DoCloseDoor();
    }
}
