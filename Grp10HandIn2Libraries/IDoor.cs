using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public interface IDoor
    {
        event EventHandler<DoorEventArgs> DoorEvent;
        void LockDoor();

        void UnlockDoor();

        void OnDoorOpen();
        void OnDoorClose();
    }
}
