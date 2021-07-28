using AnagramSolver.BusinessLogic.Classes.Services;
using AnagramSolver.BusinessLogic.Classes.Users;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
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



        [SetUp]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _userService = new UserService(_userRepository);
        }
        [Test]
        public void Should_ReadUser_When_GivenUserID()
        {
            var testUser = CreateTestUser();
            _userRepository.GetUser(Arg.Any<int>()).Returns(testUser);
            var stringToShow = $"{testUser.FirstName}  {testUser.LastName}  {testUser.Email}";

            var result = _userService.ReadUser(testUser.Id);

            Assert.AreEqual(stringToShow, result);
        }
        [Test]
        public void Should_CreateUser_When_GivenUserInfo()
        {
            var testUser = CreateTestUser();
            var favouriteWord = "neprisikiskiakopusteliaudavome";
            var wordFromDb = Word.CreateTestWord();
            var userWord = new UserWord { UserId = 1, WordId = 1 };
            var listOfUserWords = new List<UserWord>();
            listOfUserWords.Add(userWord);
            _userRepository.AddUser(testUser.FirstName, testUser.LastName, testUser.Email, testUser.Pass).Returns(testUser.Id);
            _userRepository.GetWord(favouriteWord).Returns(wordFromDb);
            _userRepository.AddUserWords(listOfUserWords);

            _userService.CreateUser(testUser.FirstName, testUser.LastName, testUser.Email, testUser.Pass, favouriteWord);

            var user = _userRepository.GetUser(testUser.Id);

            Assert.That(testUser.Email, Is.EqualTo(user.Email));
        }
        public User CreateTestUser()
        {
            var userId = 1;
            var userName = "Jonas";
            var userLastName = "Jaugelis";
            var userEmail = "abc@cdv.lt";
            var userPass = "labas";
            var user = new User { Id = userId, FirstName = userName, LastName = userLastName, Email = userEmail, Pass = userPass };
            return user;

        }
    }
}
