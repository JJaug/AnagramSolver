using NUnit.Framework;

namespace AnagramSolver.Tests
{
    [TestFixture]
    class AnagramSolverTests
    {
        private BusinessLogic.Classes.AnagramSolver _anagramSolver;

        [SetUp] public void Setup()
        {
            _anagramSolver = new BusinessLogic.Classes.AnagramSolver();
        }
        [Test]
        [TestCase("sula", "alus", "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas.txt")]
        [TestCase("alus", "sula", "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas.txt")]
        [TestCase("mirti", "rimti", "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas.txt")]
        [TestCase("rimti", "mirti", "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas.txt")]
        public void Should_ReturnOneAnagram_When_GivenOneWord(string command, string anagram, string filePath)
        {
            var allAnagrams = _anagramSolver.GetAnagrams(command, 4, @filePath);

            Assert.That(allAnagrams[0].Word, Is.EqualTo(anagram));
        }
    }
}
