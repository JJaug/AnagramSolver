using GenericsTask;
using NUnit.Framework;
using System;
using static GenericsTask.Enums.Enum;

namespace AnagramSolver.Tests
{
    [TestFixture]
    class GenericsTests
    {

        [Test]
        [TestCase("Tuesday")]
        public void Should_ReturnCorrectEnum_When_GivenDay(string day)
        {
            var generics = new GenericsTask<Weekday>();
            Assert.That(generics.MapValueToEnum(day), Is.EqualTo(Weekday.Tuesday));
        }
        [Test]
        [TestCase("Male")]
        public void Should_ReturnCorrectEnum_When_GivenGender(string gender)
        {
            var generics = new GenericsTask<Gender>();
            Assert.That(generics.MapValueToEnum(gender), Is.EqualTo(Gender.Male));
        }
        [Test]
        [TestCase("labas")]
        public void Should_ReturnThrowSentence_When_GivenWordDoesNotBelongToEnum(string word)
        {
            var generics = new GenericsTask<Gender>();
            Exception ex = Assert.Throws<Exception>(
            delegate { throw new Exception($"Value '{word}' is not part of Gender enum"); });

            Assert.That(ex.Message, Is.EqualTo($"Value '{word}' is not part of Gender enum"));
        }
    }
}

