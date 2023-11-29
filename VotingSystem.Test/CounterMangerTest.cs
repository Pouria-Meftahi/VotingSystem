namespace VotingSystem.Test
{
    public class CounterMangerTest
    {
        public const string CounterName = "Counter Name";
        public Counter _counter = new()
        {
            Name = CounterName,
            Count = 5,
        };


        [Fact]
        public void GetCounterStatistics_IncludesCounterName()
        {
            var statistics = new CounterManager().GetStatistics(_counter,5);
            Assert.Equal(CounterName, _counter.Name);
        }

        [Fact]
        public void GetCounterStatistics_IncludesCounterCount()
        {
            var statistics = new CounterManager().GetStatistics(_counter, 5);
            Assert.Equal(5, statistics.Count);
        }

        [Theory]
        [InlineData(5, 10, 50)]
        [InlineData(1, 3, 33.33)]
        [InlineData(2, 3, 66.67)]
        [InlineData(2, 8, 25)]
        public void GetCounterStatistics_ShowsPercentageBasedOnTotalCount(int count, int total, double expected)
        {
            _counter.Count = count;
            var statistics = new CounterManager().GetStatistics(_counter, total);
            Assert.Equal(expected, statistics.Percent);
        }

        [Fact]
        public void ResolveExcess_DoesntAddExcessWhenAllCountersAreEqual()
        {
            var counter1 = new Counter { Percent = 33.33 };
            var counter2 = new Counter { Percent = 33.33 };
            var counter3 = new Counter { Percent = 33.33 };
            var counters = new List<Counter> { counter1, counter2, counter3 };
            new CounterManager().ResolveExcess(counters);
            Assert.Equal(33.33, counter1.Percent);
        }

        [Theory]
        [InlineData(66.66, 66.67, 33.33)]
        [InlineData(66.65, 66.67, 33.33)]
        [InlineData(66.66, 66.68, 33.32)]
        public void ResolveExcessAddsExcessToHighestCounter(double intial, double expected, double lowest)
        {
            var counter1 = new Counter {  Percent = intial };
            var counter2 = new Counter {  Percent = lowest };
            var counters = new List<Counter> { counter1, counter2 };
            new CounterManager().ResolveExcess(counters);
            Assert.Equal(expected, counter1.Percent);
            Assert.Equal(lowest, counter2.Percent);
            
            var counter3 = new Counter {  Percent = intial };
            var counter4 = new Counter {  Percent = lowest };
            counters = new List<Counter> { counter4, counter3 };
            new CounterManager().ResolveExcess(counters);
            Assert.Equal(expected, counter3.Percent);
            Assert.Equal(lowest, counter4.Percent);
        }
        [Theory]
        [InlineData(11.11, 11.12, 44.44)]
        [InlineData(11.10, 11.12, 44.44)]
        public void ResolveExcees_AddsExcessToLowestCounterWhenMoreThenOneHighestCounters(double init,double expected ,double highest)
        {
            var counter1 = new Counter { Percent = highest };
            var counter2 = new Counter { Percent = highest };
            var counter3 = new Counter { Percent = init };
            var counters = new List<Counter> { counter1, counter2, counter3 };
            new CounterManager().ResolveExcess(counters);
            Assert.Equal(highest, counter1.Percent);
            Assert.Equal(highest, counter2.Percent);
            Assert.Equal(expected, counter3.Percent);
        }
        [Fact]
        public void ResolveExcess_DoesntAddExcessIfTotalPercentIs100()
        {
            var counter1 = new Counter {Percent = 80 };
            var counter2 = new Counter {Percent = 20 };
            var counters = new List<Counter> { counter1, counter2 };
            new CounterManager().ResolveExcess(counters);
            Assert.Equal(80, counter1.Percent);
            Assert.Equal(20, counter2.Percent);
        }
    }

    public class Counter
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double Percent { get; set; }
      
    }
    public class CounterManager
    {
        internal Counter GetStatistics(Counter counter,int totalCount)
        {
            counter.Percent = RoundUp(counter.Count * 100.0 / totalCount);
            return counter;
        }

        internal void ResolveExcess(List<Counter> counters)
        {
            var totalPercent = counters.Sum(s => s.Percent);
            if (totalPercent == 100) return;
            var excess = 100 - totalPercent;
            var highestPercent = counters.Max(a => a.Percent);
            var highestCounters = counters.Where(a => a.Percent == highestPercent).ToList();

            if (highestCounters.Count == 1)
            {
               highestCounters.First().Percent += excess;
            }
            else if (highestCounters.Count < counters.Count)
            {
                var lowestPercent = counters.Min(s => s.Percent);
                var lowestCounter = counters.First(a => a.Percent == lowestPercent);
                lowestCounter.Percent = RoundUp(lowestCounter.Percent+excess);
            }
        }
        private static double RoundUp(double num) => Math.Round(num, 2);
    }
}
