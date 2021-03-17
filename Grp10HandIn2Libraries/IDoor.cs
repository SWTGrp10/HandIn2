using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    interface IDoor
    {
        void LockDoor();

        void UnlockDoor();

        void OnDoorOpen();
        void OnDoorClose();
    }
}
