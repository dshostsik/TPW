using Data;
using Logic;
using System.Runtime.CompilerServices;

namespace TestLogic
{
    internal class dataAbstract : DataAbstract
    {

    }

    [TestClass]
    public class UnitTest1
    {

        private DataAbstract data;
        private LogicAbstractAPI table;

        public void initialiseVariables()
        {
            data = dataAbstract.init();
            table = LogicAbstractAPI.initialize(data);
        }

        [TestMethod]
        public void TestTableNBall()
        {
           initialiseVariables();
            table.addBalls(1, 20);
            Assert.AreEqual(table.GetBalls().Count, 1);
            Assert.AreEqual(table.GetBalls().ElementAt(0).radius, 20);
            table.removeBalls();
            Assert.AreEqual(table.GetBalls().Count, 0);
        }
    }
}