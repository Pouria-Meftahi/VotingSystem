namespace VotingSystem.Test
{
    public class MathOne
    {
         
        public MathOne()
        {
            
        }
    }
    public interface ITestSample
    {
        public int Add(int a, int b);
    }
    public class TestSample:ITestSample
    {
        public int Add(int a, int b) => a + b;
    }
    
    public class UnitTest1
    {

        [Theory]
        [InlineData(1,1,2)]
        [InlineData(2,3,5)]
        public void Test1(int a ,int b,int expected)
        {
            var result = new TestSample().Add(a,b);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void TestListContainsValue()
        {
            var list = new List<int> { 1, 2, 3, 5 };
            Assert.Contains(1, list);
        }
    }
}