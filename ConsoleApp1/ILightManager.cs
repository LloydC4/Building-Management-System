using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDev
{
    public interface ILightManager
    {
        public void SetLight(bool isOn, int lightID)
        {

        }

        public void SetAllLights(bool isOn)
        {

        }
        public string GetStatus()
        {
            string status = "Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,";
            return status;
        }
    }
}
