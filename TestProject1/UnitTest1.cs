namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var greeting = new ClassLibrary1.Greeting("Hello, World!");
            Assert.AreEqual(greeting.output(), "Hello, World!");
        }

        [TestMethod]
        public void test() {
            var a = new ABC.Calculator3();
            int result = a.add(3, 2);
            Assert.AreEqual(5, result);
            result = a.add(2, 3);
            Assert.AreEqual(5, result);
        }
    }
}