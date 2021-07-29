using AnagramSolver.BusinessLogic.Classes.Users;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Tests.Helpers;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace AnagramSolver.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserRepository _userRepository;
        private IUserService _userService;
        private GetTestWords _testWords;

        [SetUp]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _userService = new UserServices(_userRepository);
            _testWords = new GetTestWords();
        }
        [Test]
        public void Should_ReadUser_When_GivenUserID()
        {
            var testUser = _testWords.CreateTestUser();
            _userRepository.GetUser(Arg.Any<long>()).Returns(testUser);
            var stringToShow = $"{testUser.FirstName}  {testUser.LastName}  {testUser.Email}";

            var result = _userService.ReadUser(testUser.Id);

            Assert.AreEqual(stringToShow, result);
        }
        [Test]
        public void Should_CreateUser_When_GivenUserInfo()
        {
            var testUser = _testWords.CreateTestUser();
            var favouriteWord = "neprisikiskiakopusteliaudavome";
            var wordFromDb = Word.CreateTestWord();
            var userWord = new UserWord { UserId = 1, WordId = 1 };
            var listOfUserWords = new List<UserWord>();
            listOfUserWords.Add(userWord);
            _userRepository.AddUser(testUser.FirstName, testUser.LastName, testUser.Email, testUser.Pass).Returns(testUser.Id);
            _userRepository.GetWord(favouriteWord).Returns(wordFromDb);
            _userRepository.AddUserWords(listOfUserWords).Returns(true);

            var isSuccessful = _userService.CreateUser(testUser.FirstName, testUser.LastName, testUser.Email, testUser.Pass, favouriteWord);

            Assert.IsTrue(isSuccessful);
        }

    }
}
