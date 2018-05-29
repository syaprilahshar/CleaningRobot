using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleaningRobot.Lib;
using Newtonsoft.Json;

namespace CleaningRobot.Tests
{
    [TestClass]
    public class TestTask1
    {
        string task1 = "{\"map\":[[\"S\",\"S\",\"S\",\"S\"],[\"S\",\"S\",\"C\",\"S\"],[\"S\",\"S\",\"S\",\"S\"],[\"S\",\"null\",\"S\",\"S\"]],\"start\":{\"X\":3,\"Y\":0,\"facing\":\"N\"},\"commands\":[\"TL\",\"A\",\"C\",\"A\",\"C\",\"TR\",\"A\",\"C\"],\"battery\":80}";
        Lib.CleaningRobot robot;
        Request request;
        Result result;

        [TestInitialize]
        public void TestInit()
        {
            robot = new Lib.CleaningRobot();
            request = new Request();
            request = JsonConvert.DeserializeObject<Request>(task1);
            result = robot.Run(request);

        }

        [TestMethod()]
        public void TestTask1_IsNull()
        {
            request = null;
            result = robot.Run(request);

            Assert.IsNull(result);
        }
        [TestMethod()]
        public void TestTask1_IsNotNull()
        {
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void TestTask1_Visited()
        {
            Assert.IsNotNull(result.Visited);
            Assert.AreEqual(result.Visited.Count, 3);
        }
        [TestMethod()]
        public void TestTask1_Cleaned()
        {
            Assert.IsNotNull(result.Cleaned);
            Assert.AreEqual(result.Cleaned.Count, 2);
        }
        [TestMethod()]
        public void TestTask1_FinalCoordinate()
        {
            Assert.IsNotNull(result.Final);
            Assert.AreEqual(result.Final.X, 2);
            Assert.AreEqual(result.Final.Y, 0);
        }
        [TestMethod()]
        public void TestTask2_FinalFacing()
        {
            Assert.IsNotNull(result.Final);
            Assert.AreEqual(result.Final.Facing, "E");
        }
        [TestMethod()]
        public void TestTask1_BatteryLeft()
        {
            Assert.AreEqual(result.Battery, 54);
        }
        [TestMethod()]
        public void TestTask1_MapIsNull()
        {
            request.Map = null;
            result = robot.Run(request);

            Assert.IsNull(result);
        }
        [TestMethod()]
        public void TestTask1_MapIsNullInFirstRow()
        {
            request.Map[0] = null;
            result = robot.Run(request);

            Assert.IsNull(result);
        }
        [TestMethod()]
        public void TestTask1_IsNotValid()
        {
            request.Map[0] = new[] { "X" };
            result = robot.Run(request);

            Assert.IsNull(result);
        }
        [TestMethod()]
        public void TestTask1_IsAnotherNotVaild()
        {
            request.Map[0] = new[] { "a", "b", "A", "A" };
            result = robot.Run(request);
            
            Assert.IsNull(result);
        }
    }
}
