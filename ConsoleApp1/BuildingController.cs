using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDev
{
    class BuildingController
    {
        private string buildingID; // stores building ID
        private string currentState; // stores current state
        private string previousState; // stores previous state to return to from fire alarm/fire drill
        ILightManager iLightManager;
        IFireAlarmManager iFireAlarmManager;
        IDoorManager iDoorManager;
        IWebService iWebService;
        IEmailService iEmailService;

        // constructor, sets ID
        public BuildingController(string id) 
        {
            this.buildingID = id.ToLower();
            this.currentState = "out of hours";
        }

        // constructor, sets ID and current state
        public BuildingController(string id, string startState)
        {
            if (startState.ToLower() == "closed" || startState.ToLower() == "open" || startState.ToLower() == "out of hours")
            {
                this.buildingID = id.ToLower();
                this.currentState = startState.ToLower();
            }
            else
            {
                throw new ArgumentException("Argument Exception: BuildingController can only be initialised to the following states 'open', 'closed', 'out of hours'");
            }
        }

        // constructor with dependency injection
        public BuildingController(string id, ILightManager iLightManager, IFireAlarmManager iFireAlarmManager, IDoorManager iDoorManager, IWebService iWebService, IEmailService iEmailService)
        {
            this.buildingID = id.ToLower();
            this.currentState = "out of hours";
            this.iLightManager = iLightManager;
            this.iFireAlarmManager = iFireAlarmManager;
            this.iDoorManager = iDoorManager;
            this.iWebService = iWebService;
            this.iEmailService = iEmailService;
        }

        // desctructor to ensure children cannot exist without parent
        ~BuildingController()
        {
            this.iLightManager = null;
            this.iFireAlarmManager = null;
            this.iDoorManager = null;
        }

        // returns currnet state
        public string GetCurrentState()
        {
            return currentState;
        }

        // sets current state
        public bool SetCurrentState(string state)
        {
            // if current state is the same, just return
            if (currentState == state.ToLower())
            {
                return true;
            }
            else if (currentState == "closed")
            {
                // possible states building can more to from closed
                string[] possibleStates = { "out of hours", "fire drill", "fire alarm" };
                for (int i = 0; i < possibleStates.Length; i++)
                {
                    if (state.ToLower() == possibleStates[i])
                    {
                        if (state.ToLower() == "fire alarm")
                        {
                            iFireAlarmManager.SetAlarm(true);
                            iDoorManager.OpenAllDoors();
                            iLightManager.SetAllLights(true);

                            // try to log fire alarm, if unable to, send email log
                            try
                            {
                                iWebService.LogFireAlarm("fire alarm");
                            }
                            catch (Exception e)
                            {
                                iEmailService.SendMail("smartbuilding@uclan.ac.uk", "failed to log alarm", e.Message);
                            }

                            previousState = currentState;
                            currentState = state.ToLower();
                            return true;
                        }
                        else
                        {
                            previousState = currentState;
                            currentState = state.ToLower();
                            return true;
                        }
                    }
                }
                return false;
            }
            else if (currentState == "out of hours")
            {
                // possible states building can more to from out of hours
                string[] possibleStates = { "closed", "open", "fire drill", "fire alarm" };
                for (int i = 0; i < possibleStates.Length; i++)
                {
                    if (state.ToLower() == possibleStates[i])
                    {
                        if (state.ToLower() == "open")
                        {
                            // if all doors can open, change state, if not, return false
                            bool allDoorsOpened = iDoorManager.OpenAllDoors();
                            if (allDoorsOpened)
                            {
                                previousState = currentState;
                                currentState = state.ToLower();
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        // lock all doors, turn off lights, then close
                        else if (state.ToLower() == "closed")
                        {
                            iDoorManager.LockAllDoors();
                            iLightManager.SetAllLights(false);
                            previousState = currentState;
                            currentState = state.ToLower();
                            return true;
                        }
                        else if (state.ToLower() == "fire alarm")
                        {
                            iFireAlarmManager.SetAlarm(true);
                            iDoorManager.OpenAllDoors();
                            iLightManager.SetAllLights(true);

                            // try to log fire alarm, if unable to, send email log
                            try
                            {
                                iWebService.LogFireAlarm("fire alarm");
                            }
                            catch (Exception e)
                            {
                                iEmailService.SendMail("smartbuilding@uclan.ac.uk", "failed to log alarm", e.Message);
                            }

                            previousState = currentState;
                            currentState = state.ToLower();
                            return true;
                        }
                        else
                        {
                            previousState = currentState;
                            currentState = state.ToLower();
                            return true;
                        }
                    }
                }
                return false;
            }
            else if (currentState == "open")
            {
                // possible states building can more to from open
                string[] possibleStates = { "out of hours", "fire drill", "fire alarm" };
                for (int i = 0; i < possibleStates.Length; i++)
                {
                    if (state.ToLower() == possibleStates[i])
                    {
                        // lock all doors, turn off lights, then close
                        if (state.ToLower() == "closed")
                        {
                            iDoorManager.LockAllDoors();
                            iLightManager.SetAllLights(false);
                            previousState = currentState;
                            currentState = state.ToLower();
                            return true;
                        }
                        else if (state.ToLower() == "fire alarm")
                        {
                            iFireAlarmManager.SetAlarm(true);
                            iDoorManager.OpenAllDoors();
                            iLightManager.SetAllLights(true);

                            // try to log fire alarm, if unable to, send email log
                            try
                            {
                                iWebService.LogFireAlarm("fire alarm");
                            }
                            catch (Exception e)
                            {
                                iEmailService.SendMail("smartbuilding@uclan.ac.uk", "failed to log alarm", e.Message);
                            }

                            previousState = currentState;
                            currentState = state.ToLower();
                            return true;
                        }
                        else
                        {
                            previousState = currentState;
                            currentState = state.ToLower();
                            return true;
                        }
                    }
                }
                return false;
            }
            else if (currentState == "fire drill")
            {
                if (state.ToLower() == previousState)
                {
                    if (state.ToLower() == "open")
                    {
                        // if all doors can open, change state, if not, return false
                        bool allDoorsOpened = iDoorManager.OpenAllDoors();
                        if (allDoorsOpened)
                        {
                            previousState = currentState;
                            currentState = state.ToLower();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    // lock all doors, turn off lights, then close
                    else if (state.ToLower() == "closed")
                    {
                        iDoorManager.LockAllDoors();
                        iLightManager.SetAllLights(false);
                        previousState = currentState;
                        currentState = state.ToLower();
                        return true;
                    }
                    else
                    {
                        previousState = currentState;
                        currentState = state.ToLower();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (currentState == "fire alarm")
            {
                if (state.ToLower() == previousState)
                {
                    if (state.ToLower() == "open")
                    {
                        // if all doors can open, change state, if not, return false
                        bool allDoorsOpened = iDoorManager.OpenAllDoors();
                        if (allDoorsOpened)
                        {
                            previousState = currentState;
                            currentState = state.ToLower();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    // lock all doors, turn off lights, then close
                    else if (state.ToLower() == "closed")
                    {
                        iDoorManager.LockAllDoors();
                        iLightManager.SetAllLights(false);
                        previousState = currentState;
                        currentState = state.ToLower();
                        return true;
                    }
                    else
                    {
                        previousState = currentState;
                        currentState = state.ToLower();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // returns building ID
        public string GetBuildingID()
        {
            return buildingID;
        }

        // sets building ID
        public void SetBuildingID(string id)
        {
            buildingID = id.ToLower();
        }

        // returns Status report, sends for engineer to repair if required
        public string GetStatusReport()
        {
            string lightStatus = iLightManager.GetStatus();
            string doorStatus = iDoorManager.GetStatus();
            string fireAlarmStatus = iFireAlarmManager.GetStatus();
            string statusReport = lightStatus + doorStatus + fireAlarmStatus;
            string needsRepair = "null";

            // checking what needs repairing
            if (lightStatus.Contains("FAULT"))
            {
                needsRepair = "Lights,";
            }
            if (doorStatus.Contains("FAULT"))
            {
                if (needsRepair == "null")
                {
                    needsRepair = "Doors,";
                }
                else
                {
                    needsRepair += "Doors,";
                } 
            }
            if (fireAlarmStatus.Contains("FAULT"))
            {
                if (needsRepair == "null")
                {
                    needsRepair = "FireAlarm,";
                }
                else
                {
                    needsRepair += "FireAlarm,";
                }
            }

            if (needsRepair != "null")
            {
                iWebService.LogEngineerRequired(needsRepair);
            }

            return statusReport;
        }
    }
}
