using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDev
{
    public interface IFireAlarmManager
    {
        public void SetAlarm(bool isActive)
        {

        }

        public string GetStatus()
        {
            string status = "FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,";
            return status;
        }
    }
}
