using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleaningRobot.Lib;
using Newtonsoft.Json;

namespace CleaningRobot.Tests
{
    [TestClass]
    public class TestTask2
    {
        string task2 = "{\"map\":[[\"S\",\"S\",\"S\",\"S\"],[\"S\",\"S\",\"C\",\"S\"],[\"S\",\"S\",\"S\",\"S\"],[\"S\",\"null\",\"S\",\"S\"]],\"start\":{\"X\":3,\"Y\":1,\"facing\":\"S\"},\"commands\":[\"TR\",\"A\",\"C\",\"A\",\"C\",\"TR\",\"A\",\"C\"],\"battery\":1094}";
        Lib.CleaningRobot robot;
        Request request;
        Result result;

        [TestInitialize]
        public void TestInit()
        {
            robot = new Lib.CleaningRobot();
            request = new Request();
            request = JsonConvert.DeserializeObject<Request>(task2);
            result = robot.Run(request);

        }

        [TestMethod()]
        public void TestTask2_IsNull()
        {
            request = null;
            result = robot.Run(request);

            Assert.IsNull(result);
        }
        [TestMethod()]
        public void TestTask2_IsNotNull()
        {
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void TestTask2_Visited()
        {
            Assert.IsNotNull(result.Visited);
            Assert.AreEqual(result.Visited.Count, 4);
        }
        [TestMethod()]
        public void TestTask2_Cleaned()
        {
            Assert.IsNotNull(result.Cleaned);
            Assert.AreEqual(result.Cleaned.Count, 3);
        }
        [TestMethod()]
        public void TestTask2_FinalCoordinate()
        {
            Assert.IsNotNull(result.Final);
            Assert.AreEqual(result.Final.X, 3);
            Assert.AreEqual(result.Final.Y, 2);
        }
        [TestMethod()]
        public void TestTask2_FinalFacing()
        {
            Assert.IsNotNull(result.Final);
            Assert.AreEqual(result.Final.Facing, "E");
        }
        [TestMethod()]
        public void TestTask2_BatteryLeft()
        {
            Assert.AreEqual(result.Battery, 1040);
        }
        [TestMethod()]
        public void TestTask2_MapIsNull()
        {
            request.Map = null;
            result = robot.Run(request);

            Assert.IsNull(result);
        }
        [TestMethod()]
        public void TestTask2_MapIsNullInFirstRow()
        {
            request.Map[0] = null;
            result = robot.Run(request);

            Assert.IsNull(result);
        }
        [TestMethod()]
        public void TestTask2_IsNotValid()
        {
            request.Map[0] = new[] { "X" };
            result = robot.Run(request);

            Assert.IsNull(result);
        }
        [TestMethod()]
        public void TestTask2_IsAnotherNotVaild()
        {
            request.Map[0] = new[] { "a", "b", "A", "A" };
            result = robot.Run(request);
            
            Assert.IsNull(result);
        }
    }
}
