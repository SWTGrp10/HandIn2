using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public interface IDoor
    {
        event EventHandler<DoorEventArgs> DoorChangedEvent;
        void LockDoor();

        void UnlockDoor();
    }
}
