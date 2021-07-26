using GenericsTask;
using NUnit.Framework;
using static GenericsTask.Enums.Enum;

namespace AnagramSolver.Tests
{
    [TestFixture]
    class GenericsTests
    {

        [Test]
        [TestCase("Tuesday")]
        public void Should_ReturnCorrectEnum_When_GivenWordFromArray(string day)
        {
            var x = new GenericsTask<Weekday>();
            Assert.That(x.MapValueToEnum(day), Is.EqualTo(Weekday.Tuesday));
        }
    }
}

