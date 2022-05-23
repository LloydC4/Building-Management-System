using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDev
{
    public interface IDoorManager
    {
        public bool OpenDoor(int doorID)
        {
            return true;
        }

        public bool LockDoor(int doorID)
        {
            return true;
        }

        virtual public bool OpenAllDoors()
        {
            return true;
        }

        virtual public bool LockAllDoors()
        {
            return true;
        }

        public string GetStatus()
        {
            string status = "Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,";
            return status;
        }
    }
}
