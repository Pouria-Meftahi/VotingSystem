using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Xunit.Assert;

namespace VotingSystem.Test
{
    public class VotingPollTests
    {
        [Fact]
        public void ZeroCountersWhenCreated()
        {
            var poll = new VotingPoll();
            Empty(poll.Counters);

        }

    }
    
    public class VotingPollFactoryTest
    {
        [Fact]
        public void Create_ThrowsWhenLessThenTowCounterNames()
        {
            var names = new[] { "name" };
            var factory = new VotingPollFactory();

            Throws<ArgumentException>(() => factory.Create("",["name"]));
            Throws<ArgumentException>(() => factory.Create("",[]));
        }

        [Fact]
        public void Create_AddCounterToThePollForEachName()
        {
            var names = new[] { "name1" , "name2" };
            var factory = new VotingPollFactory();
            var poll = factory.Create("",names);
            foreach (var item in names)
            {
               Contains(item, poll.Counters.Select(x => x.Name));
            }
        }

        [Fact]
        public void Create_AddsTitleToThePoll()
        {
            var title = "title";
            var names = new[] { "name1","name2"};
            var factory = new VotingPollFactory();
            var poll = factory.Create(title,names);
        }
    }
    public class VotingPollFactory
    {
        public VotingPollFactory()
        {
            
        }
        public VotingPoll Create(string title ,string[] names)
        {
            if (names.Length < 2) throw new ArgumentException();

            return new VotingPoll
            {
                Counters = names.Select(name => new Counter { Name = name })
            };
        }
    }

    public class VotingPoll
    {
        public VotingPoll()
        {
            Counters = Enumerable.Empty<Counter>();
        }
        public IEnumerable<Counter> Counters { get; set; }
    }
}
