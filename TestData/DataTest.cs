using Data;

namespace TestData
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            DataAbstract data = DataAbstract.init();
            Assert.IsNotNull(data);
        }
    }
}