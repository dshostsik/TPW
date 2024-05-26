using Data;

namespace TestData
{
    [TestClass]
    public class DataTest
    {
        DataAbstract data = DataAbstract.init(600, 600);
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsNotNull(data);
            data.createBalls(2);
            Assert.IsTrue(data.getAmountOfBalls() == 2);
        }
    }
}