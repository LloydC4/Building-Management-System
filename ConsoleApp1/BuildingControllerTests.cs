using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NSubstitute;

namespace SoftwareDev
{
    [TestFixture]
    class BuildingControllerTests
    {
        
        // SINGLE PARAMETER CONSTRUCTOR TESTS
        //L1R5
        [TestCase]
        public void DoesConstructorSetStatus()
        {
            //arrange/act
            BuildingController sut = new BuildingController("test");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L1R1
        [TestCase]
        public void DoesConstructorSetID()
        {
            //arrange/act
            BuildingController sut = new BuildingController("test");
            //assert
            Assert.That(sut.GetBuildingID(), Is.EqualTo("test"));
        }
        //L1R3
        [TestCase]
        public void DoesConstructorSetIDCapitals()
        {
            //arrange/act
            BuildingController sut = new BuildingController("TEST");
            //assert
            Assert.That(sut.GetBuildingID(), Is.EqualTo("test"));
        }
        // TWO PARAMETER CONSTRUCTOR TESTS
        //L2R3
        [TestCase]
        public void DoesConstructorSetStatusToClosed()
        {
            //arrange/act
            BuildingController sut = new BuildingController("test", "closed");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("closed"));
        }
        //L2R3
        [TestCase]
        public void DoesConstructorSetStatusToClosedCapitals()
        {
            //arrange/act
            BuildingController sut = new BuildingController("test", "CLOSED");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("closed"));
        }
        //L2R3
        [TestCase]
        public void DoesConstructorSetStatusToOpen()
        {
            //arrange/act
            BuildingController sut = new BuildingController("test", "open");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("open"));
        }
        //L2R3
        [TestCase]
        public void DoesConstructorSetStatusToOpenCapitals()
        {
            //arrange/act
            BuildingController sut = new BuildingController("test", "OPEN");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("open"));
        }
        //L2R3
        [TestCase]
        public void DoesConstructorSetStatusToOutOfHours()
        {
            //arrange/act
            BuildingController sut = new BuildingController("test", "out of hours");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L2R3
        [TestCase]
        public void DoesConstructorSetStatusToOutOfHoursCapitals()
        {
            //arrange/act
            BuildingController sut = new BuildingController("test", "OUT OF HOURS");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L2R3
        [TestCase]
        public void ConstructorExceptionTest()
        {
            //arrange/act/assert
            Assert.Throws(Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo("Argument Exception: BuildingController can only be initialised to the following states 'open', 'closed', 'out of hours'"),
              () => new BuildingController("test", "rtrtfgfg"));
        }
        //L2R3
        [TestCase]
        public void DoesConstructorSetID2()
        {
            //arrange/act
            BuildingController sut = new BuildingController("test", "closed");
            //assert
            Assert.That(sut.GetBuildingID(), Is.EqualTo("test"));
        }
        //L2R3
        [TestCase]
        public void DoesConstructorSetIDCapitals2()
        {
            //arrange/act
            BuildingController sut = new BuildingController("TEST", "closed");
            //assert
            Assert.That(sut.GetBuildingID(), Is.EqualTo("test"));
        }
        // BUILDING CONTROLLER TESTS
        //L1R6
        [TestCase]
        public void GetCurrentStateTest()
        {
            //arrange/act
            BuildingController sut = new BuildingController("test");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L1R2
        [TestCase]
        public void GetBuildingIDTest()
        {
            //arrange/act
            BuildingController sut = new BuildingController("test");
            //assert
            Assert.That(sut.GetBuildingID(), Is.EqualTo("test"));
        }
        //L1R4
        [TestCase]
        public void SetBuildingIDCapitalsTest()
        {
            //arrange
            BuildingController sut = new BuildingController("test");
            //act
            sut.SetBuildingID("TEST");
            //assert
            Assert.That(sut.GetBuildingID(), Is.EqualTo("test"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromClosedToOutOfHours()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "closed");
            //act
            sut.SetCurrentState("out of hours");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromClosedToOutOfHoursCapital()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "closed");
            //act
            sut.SetCurrentState("OUT OF HOURS");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromClosedToFireDrill()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "closed");
            //act
            sut.SetCurrentState("fire drill");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire drill"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromClosedToFireDrillCapital()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "closed");
            //act
            sut.SetCurrentState("FIRE DRILL");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire drill"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromClosedToFireAlarm()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            sut.SetCurrentState("fire alarm");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire alarm"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromClosedToFireAlarmCapital()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            sut.SetCurrentState("FIRE ALARM");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire alarm"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromClosedToNA()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "closed");
            //act
            sut.SetCurrentState("rttgfgt");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("closed"));
        }
        //L2R2
        [TestCase]
        public void StateMoveFromClosedToClosed()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "closed");
            //act
            sut.SetCurrentState("closed");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("closed"));
        }
        //L2R2
        [TestCase]
        public void StateMoveFromClosedToClosedCapital()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "closed");
            //act
            sut.SetCurrentState("CLOSED");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("closed"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOutOfHoursToClosed()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("closed"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOutOfHoursToClosedCapital()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("CLOSED");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("closed"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOutOfHoursToOpen()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            doorManager.OpenAllDoors().Returns(true);
            sut.SetCurrentState("open");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("open"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOutOfHoursToOpenCapital()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            doorManager.OpenAllDoors().Returns(true);
            sut.SetCurrentState("OPEN");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("open"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOutOfHoursToFireDrill()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "out of hours");
            //act
            sut.SetCurrentState("fire drill");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire drill"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOutOfHoursToFireDrillCapital()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "out of hours");
            //act
            sut.SetCurrentState("FIRE DRILL");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire drill"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOutOfHoursToFireAlarm()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("fire alarm");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire alarm"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOutOfHoursToFireAlarmCapital()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("FIRE ALARM");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire alarm"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOutOfHoursToNA()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "out of hours");
            //act
            sut.SetCurrentState("rttgfgt");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L2R2
        [TestCase]
        public void StateMoveFromOutOfHoursToOutOfHours()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "out of hours");
            //act
            sut.SetCurrentState("out of hours");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L2R2
        [TestCase]
        public void StateMoveFromOutOfHoursToOutOfHoursCapital()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "out of hours");
            //act
            sut.SetCurrentState("OUT OF HOURS");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOpenToOutOfHours()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "open");
            //act
            sut.SetCurrentState("out of hours");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOpenToOutOfHoursCapital()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "open");
            //act
            sut.SetCurrentState("OUT OF HOURS");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOpenToFireDrill()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "open");
            //act
            sut.SetCurrentState("fire drill");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire drill"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOpenToFireDrillCapital()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "open");
            //act
            sut.SetCurrentState("FIRE DRILL");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire drill"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOpenToFireAlarm()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("open");
            sut.SetCurrentState("fire alarm");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire alarm"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOpenToFireAlarmCapital()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("open");
            sut.SetCurrentState("FIRE ALARM");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire alarm"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromOpenToNA()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "open");
            //act
            sut.SetCurrentState("rttgfgt");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("open"));
        }
        //L2R2
        [TestCase]
        public void StateMoveFromOpenToOpen()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "open");
            //act
            sut.SetCurrentState("open");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("open"));
        }
        //L2R2
        [TestCase]
        public void StateMoveFromOpenToOpenCapital()
        {
            //arrange
            BuildingController sut = new BuildingController("test", "open");
            //act
            sut.SetCurrentState("OPEN");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("open"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromFireDrillToPreviousState()
        {
            //arrange
            BuildingController sut = new BuildingController("test");
            //act
            sut.SetCurrentState("fire drill");
            sut.SetCurrentState("out of hours");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromFireDrillToNonPreviousState()
        {
            //arrange
            BuildingController sut = new BuildingController("test");
            //act
            sut.SetCurrentState("fire drill");
            sut.SetCurrentState("closed");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire drill"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromFireDrillToPreviousStateCapital()
        {
            //arrange
            BuildingController sut = new BuildingController("test");
            //act
            sut.SetCurrentState("fire drill");
            sut.SetCurrentState("OUT OF HOURS");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromFireDrillToNA()
        {
            //arrange
            BuildingController sut = new BuildingController("test");
            //act
            sut.SetCurrentState("fire drill");
            sut.SetCurrentState("rttgfgt");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire drill"));
        }
        //L2R2
        [TestCase]
        public void StateMoveFromFireDrillToFireDrill()
        {
            //arrange
            BuildingController sut = new BuildingController("test");
            //act
            sut.SetCurrentState("fire drill");
            sut.SetCurrentState("fire drill");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire drill"));
        }
        //L2R2
        [TestCase]
        public void StateMoveFromFireDrillToFireDrillCapital()
        {
            //arrange
            BuildingController sut = new BuildingController("test");
            //act
            sut.SetCurrentState("fire drill");
            sut.SetCurrentState("FIRE DRILL");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire drill"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromFireAlarmToPreviousState()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("fire alarm");
            sut.SetCurrentState("out of hours");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromFireAlarmToNonPreviousState()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("fire alarm");
            sut.SetCurrentState("closed");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire alarm"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromFireAlarmToPreviousStateCapital()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("fire alarm");
            sut.SetCurrentState("OUT OF HOURS");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L1R7/L2R1
        [TestCase]
        public void StateMoveFromFireAlarmToNA()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("fire alarm");
            sut.SetCurrentState("rttgfgt");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire alarm"));
        }
        //L2R2
        [TestCase]
        public void StateMoveFromFireAlarmToFireAlarm()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("fire alarm");
            sut.SetCurrentState("fire alarm");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire alarm"));
        }
        //L2R2
        [TestCase]
        public void StateMoveFromFireAlarmToFireAlarmCapital()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("fire alarm");
            sut.SetCurrentState("FIRE ALARM");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("fire alarm"));
        }
        //L3R3
        [TestCase]
        public void GetStatusReportTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            //assert
            Assert.That(sut.GetStatusReport(), Is.EqualTo("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,"));
        }
        //L3R4
        [TestCase]
        public void MoveToOpenFailureWhenDoorsDontOpen()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            doorManager.OpenAllDoors().Returns(false);
            sut.SetCurrentState("open");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("out of hours"));
        }
        //L3R5
        [TestCase]
        public void MoveToOpenSuccessWhenDoorsOpen()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            doorManager.OpenAllDoors().Returns(true);
            sut.SetCurrentState("open");
            //assert
            Assert.That(sut.GetCurrentState(), Is.EqualTo("open"));
        }
        //L4R1
        [TestCase]
        public void LockAllDoorsCalledOnCloseFromOutOfHours()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            //assert
            doorManager.Received().LockAllDoors();
        }
        //L4R1
        [TestCase]
        public void SetAllLightsCalledOnCloseFromOutOfHours()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            //assert
            lightManager.Received().SetAllLights(false);
        }
        //L4R1
        [TestCase]
        public void LockAllDoorsCalledOnCloseFromFireDrill()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            sut.SetCurrentState("fire drill");
            sut.SetCurrentState("closed");
            //assert
            doorManager.Received(2).LockAllDoors();
        }
        //L4R1
        [TestCase]
        public void SetAllLightsCalledOnCloseFromFireDrill()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            sut.SetCurrentState("fire drill");
            sut.SetCurrentState("closed");
            //assert
            lightManager.Received(2).SetAllLights(false);
        }
        //L4R1
        [TestCase]
        public void LockAllDoorsCalledOnCloseFromFireAlarm()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            sut.SetCurrentState("fire alarm");
            sut.SetCurrentState("closed");
            //assert
            doorManager.Received(2).LockAllDoors();
        }
        //L4R1
        [TestCase]
        public void SetAllLightsCalledOnCloseFromFireAlarm()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            sut.SetCurrentState("fire alarm");
            sut.SetCurrentState("closed");
            //assert
            lightManager.Received(2).SetAllLights(false);
        }
        //L4R2
        [TestCase]
        public void SetAlarmCalledOnFireAlarmFromClosed()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            sut.SetCurrentState("fire alarm");
            //assert
            fireAlarmManager.Received().SetAlarm(true);
        }
        //L4R2
        [TestCase]
        public void OpenAllDoorsCalledOnFireAlarmFromClosed()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            sut.SetCurrentState("fire alarm");
            //assert
            doorManager.Received().OpenAllDoors();
        }
        //L4R2
        [TestCase]
        public void SetAllLightsCalledOnFireAlarmFromClosed()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            sut.SetCurrentState("fire alarm");
            //assert
            lightManager.Received().SetAllLights(true);
        }
        //L4R2
        [TestCase]
        public void LogFireAlarmCalledOnFireAlarmFromClosed()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("closed");
            sut.SetCurrentState("fire alarm");
            //assert
            webService.Received().LogFireAlarm("fire alarm");
        }
        //L4R2
        [TestCase]
        public void SetAlarmCalledOnFireAlarmFromOutOfHours()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("fire alarm");
            //assert
            fireAlarmManager.Received().SetAlarm(true);
        }
        //L4R2
        [TestCase]
        public void OpenAllDoorsCalledOnFireAlarmFromOutOfHours()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("fire alarm");
            //assert
            doorManager.Received().OpenAllDoors();
        }
        //L4R2
        [TestCase]
        public void SetAllLightsCalledOnFireAlarmFromOutOfHours()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("fire alarm");
            //assert
            lightManager.Received().SetAllLights(true);
        }
        //L4R2
        [TestCase]
        public void LogFireAlarmCalledOnFireAlarmFromOutOfHours()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("fire alarm");
            //assert
            webService.Received().LogFireAlarm("fire alarm");
        }
        //L4R2
        [TestCase]
        public void SetAlarmCalledOnFireAlarmFromOpen()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("open");
            sut.SetCurrentState("fire alarm");
            //assert
            fireAlarmManager.Received().SetAlarm(true);
        }
        //L4R2
        [TestCase]
        public void OpenAllDoorsCalledOnFireAlarmFromOpen()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("open");
            sut.SetCurrentState("fire alarm");
            //assert
            doorManager.Received().OpenAllDoors();
        }
        //L4R2
        [TestCase]
        public void SetAllLightsCalledOnFireAlarmFromOpen()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("open");
            sut.SetCurrentState("fire alarm");
            //assert
            lightManager.Received().SetAllLights(true);
        }
        //L4R2
        [TestCase]
        public void LogFireAlarmCalledOnFireAlarmFromOpen()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            sut.SetCurrentState("open");
            sut.SetCurrentState("fire alarm");
            //assert
            webService.Received().LogFireAlarm("fire alarm");
        }
        //L4R3
        [TestCase]
        public void GetStatusReportNullTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            //assert
            Assert.That(sut.GetStatusReport(), Is.EqualTo("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportLightsTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,FAULT,OK,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired("Lights,");
        }
        //L4R3
        [TestCase]
        public void GetStatusReportFireAlarmTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,FAULT,OK,OK,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("FireAlarm,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportDoorsTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Doors,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportLightsDoorsTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Lights,Doors,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportLightsFireAlarmTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Lights,FireAlarm,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportDoorsFireAlarmTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Doors,FireAlarm,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportDoorsLightsTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Lights,Doors,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportFireAlarmLightsTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,FAULT,OK,OK,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Lights,FireAlarm,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportFireAlarmDoorsTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Doors,FireAlarm,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportLightsDoorsFireAlarmTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,FAULT,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Lights,Doors,FireAlarm,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportLightsFireAlarmDoorsTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,FAULT,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Lights,Doors,FireAlarm,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportDoorsLightsFireAlarmTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,FAULT,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Lights,Doors,FireAlarm,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportDoorsFireAlarmLightsTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Lights,Doors,FireAlarm,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportFireAlarmLightsDoorsTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,OK,OK,FAULT,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,FAULT,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Lights,Doors,FireAlarm,"));
        }
        //L4R3
        [TestCase]
        public void GetStatusReportFireAlarmDoorsLightsTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,FAULT,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,OK,OK,OK,");
            sut.GetStatusReport();
            //assert
            webService.Received().LogEngineerRequired(Arg.Is<string>("Lights,Doors,FireAlarm,"));
        }
        //L4R4 - not sure why this won't pass
        [TestCase]
        public void LogFireAlarmExceptionText()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            var emailService = Substitute.For<IEmailService>();
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            BuildingController sut = new BuildingController("test", lightManager, fireAlarmManager, doorManager, webService, emailService);
            //act
            var e = Assert.Throws<ArgumentNullException>(() => webService.LogFireAlarm(null));
            sut.SetCurrentState("fire alarm");
            //assert
            emailService.Received().SendMail("smartbuilding@uclan.ac.uk", "failed to log alarm", e.Message);
        }

        // DOOR MANAGER TESTS

        //L3R2
        [TestCase]
        public void DoorManagerGetStatusTest()
        {
            //arrange
            var doorManager = Substitute.For<IDoorManager>();
            //act
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            //assert
            Assert.That(doorManager.GetStatus(), Is.EqualTo("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,"));
        }


        // EMAIL SERVICE TESTS

        // FIRE ALARM MANAGER TESTS

        //L3R2
        [TestCase]
        public void FireAlarmManagerGetStatusTest()
        {
            //arrange
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            //act
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            //assert
            Assert.That(fireAlarmManager.GetStatus(), Is.EqualTo("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,"));
        }

        // LIGHT MANAGER TESTS

        //L3R2
        [TestCase]
        public void LightManagerGetStatusTest()
        {
            //arrange
            var lightManager = Substitute.For<ILightManager>();
            //act
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            //assert
            Assert.That(lightManager.GetStatus(), Is.EqualTo("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,"));
        }

        // WEB SERVICE TESTS
    }
}
