﻿using NUnit.Framework;

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
            var allAnagrams = _anagramSolver.GetAnagrams(command, 4, @_filePath);

            Assert.That(allAnagrams[0].Word, Is.EqualTo(anagram));
        }
        [Test]
        [TestCase("balos")]
        public void Should_ReturnAllAnagrams_When_GivenOneWord(string command)
        {
            var allAnagrams = _anagramSolver.GetAnagrams(command, 4, @_filePath);

            Assert.That(allAnagrams.Count, Is.GreaterThan(1));
        }
    }
}
