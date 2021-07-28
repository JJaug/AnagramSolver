using GenericsTask;
using NUnit.Framework;
using System;
using static GenericsTask.Enums.GenderEnum;
using static GenericsTask.Enums.WeekdayEnum;

namespace AnagramSolver.Tests
{
    [TestFixture]
    class GenericsTests
    {

        [Test]
        public void Should_ReturnCorrectEnum_When_GivenDay()
        {
            var day = "Tuesday";
            var generics = new GenericsTask<Weekday>();
            Assert.That(generics.MapValueToEnum(day), Is.EqualTo(Weekday.Tuesday));
        }
        [Test]
        public void Should_ReturnCorrectEnum_When_GivenGender()
        {
            var gender = "Male";
            var generics = new GenericsTask<Gender>();
            Assert.That(generics.MapValueToEnum(gender), Is.EqualTo(Gender.Male));
        }
        [Test]
        public void Should_ReturnThrowSentence_When_GivenWordDoesNotBelongToEnum()
        {
            var word = "labas";
            var generics = new GenericsTask<Gender>();
            var ex = Assert.Throws<Exception>(() => generics.MapValueToEnum(word));
            Assert.That(ex.Message, Is.EqualTo($"Value '{word}' is not part of Gender enum"));
        }
    }
}

