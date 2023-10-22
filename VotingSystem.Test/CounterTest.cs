namespace VotingSystem.Test
{
    public class CounterTest
    {
        public const string CounterName = "Counter Name";
        public Counter _counter = new()
        {
            Name = CounterName,
            Count = 5,
        };

        [Fact]
        public void HasName()
        {
            Assert.Equal(CounterName, _counter.Name);
        }

        [Fact]
        public void GetCounterStatistics_IncludesCounterName()
        {
            var statistics = _counter.GetStatistics(5);
            Assert.Equal(CounterName, _counter.Name);
        }

        [Fact]
        public void GetCounterStatistics_IncludesCounterCount()
        {
            var statistics = _counter.GetStatistics(5);
            Assert.Equal(5, statistics.Count);
        }

        [Theory]
        [InlineData(5, 10, 50)]
        [InlineData(1, 3, 33.33)]
        [InlineData(2, 3, 66.67)]
        [InlineData(2, 8, 25)]
        public void GetCounterStatistics_IncludesCounterCountTotal(int count, int total, int expected)
        {
            _counter.Count = count;
            var statistics = _counter.GetStatistics(10);
            Assert.Equal(expected, statistics.Count);
        }

        [Fact]
        public void ResolveExcess_DoesntAddExcesswhenAllCountersAreEqual()
        {
        var counter1 = new Counter {Count=1,Percent=33.33 };
        var counter2 = new Counter {Count=1,Percent=33.33 };
        var counter3 = new Counter {Count=1,Percent=33.33 };
        }

    }

    public class Counter
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double Percent { get; set; }
        internal Counter GetStatistics(int totalCount)
        {
            //if (totalCount == 10)
            //    Percent = 50;
            //else if (totalCount == 3)
            //    Percent = 33.33;
            //else if (totalCount == 8)
            //    Percent = 25;
            Percent = Math.Round(Count*100.0/totalCount, 2);
            return this;
        }
    }
}
