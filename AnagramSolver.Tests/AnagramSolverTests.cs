using NUnit.Framework;

namespace AnagramSolver.Tests
{
    [TestFixture]
    class AnagramSolverTests
    {
        private BusinessLogic.Classes.AnagramSolver _anagramSolver;
        public const string _filePath = "C:\\Users\\jonas.jaugelis\\source\\repos\\AnagramSolver\\zodynas.txt";

        [SetUp] public void Setup()
        {
            _anagramSolver = new BusinessLogic.Classes.AnagramSolver();
        }

        [Test]
        [TestCase("sula", "alus")]
        [TestCase("alus", "sula")]
        [TestCase("mirti", "rimti")]
        [TestCase("rimti", "mirti")]
        public void Should_ReturnOneAnagram_When_GivenOneWord(string command, string anagram)
        {
            var allAnagrams = _anagramSolver.GetAnagrams(command, 4, _filePath);

            Assert.That(allAnagrams[0], Is.EqualTo(anagram));
            Assert.AreEqual(anagram, allAnagrams[0]);
        }

        [Test]
        public void Should_ReturnAllAnagrams_When_GivenOneWord()
        {
            var word = "balos";
            var allAnagrams = _anagramSolver.GetAnagrams(word, 4, _filePath);

            Assert.That(allAnagrams.Count, Is.GreaterThan(1));
        }

        [Test]
        public void Should_ReturnEmptyList_When_GivenWordWithNoAnagrams()
        {
            var word = "abcd";
            var allAnagrams = _anagramSolver.GetAnagrams(word, 4, _filePath);

            Assert.That(allAnagrams, Is.Empty);
        }

        [Test]
        [TestCase("rimti sula", "mirti alus")]
        public void Should_ReturnOneAnagram_When_GivenMoreThanOneWord(string command, string anagram)
        {
            var allAnagrams = _anagramSolver.GetAnagrams(command, 4, _filePath);

            Assert.That(allAnagrams[0], Is.EqualTo(anagram));
        }

        [Test]
        [TestCase("rimti sula")]
        [TestCase("visma praktika")]
        [TestCase("labas rytas")]
        public void Should_ReturnAllAnagrams_When_GivenMoreThanOneWord(string command)
        {
            var allAnagrams = _anagramSolver.GetAnagrams(command, 4, _filePath);

            Assert.That(allAnagrams.Count, Is.GreaterThanOrEqualTo(1));
        }
    }
}
