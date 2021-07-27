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
        private UserService _userService;


        [SetUp]
        public void Setup()
        {

            _userRepository = Substitute.For<IUserRepository>();
            _userService = new UserService(_userRepository);
        }
        [Test]
        public void Should_ReadUser_When_GivenUserID()
        {
            var userId = 1;
            var userName = "Jonas";
            var userLastName = "Jaugelis";
            var userEmail = "abc@cdv.lt";
            var user = new User { Id = userId, FirstName = userName, LastName = userLastName, Email = userEmail };
            _userRepository.GetUser(2).Returns(user);
            var stringToShow = $"{user.FirstName}  {user.LastName}  {user.Email}";

            var result = _userService.ReadUser(user.Id);

            Assert.AreEqual(stringToShow, result);
        }
        [Test]
        public void Should_CreateUser_When_GivenUserInfo()
        {
            var userId = 1;
            var userName = "Jonas";
            var userLastName = "Jaugelis";
            var userEmail = "abc@cdv.lt";
            var userPass = "labas";
            var favouriteWord = "neprisikiskiakopusteliaudavome";
            var wordFromDb = Word.CreateTestWord();
            var userWord = new UserWord { UserId = 1, WordId = 1 };
            var listOfUserWords = new List<UserWord>();
            listOfUserWords.Add(userWord);
            _userRepository.AddUser(userName, userLastName, userEmail, userPass).Returns(userId);
            _userRepository.GetWord(favouriteWord).Returns(wordFromDb);
            _userRepository.AddUserWords(listOfUserWords);

            _userService.CreateUser(userName, userLastName, userEmail, userPass, favouriteWord);

            var user = _userRepository.GetUser(userId);

            Assert.That(user.Email, Is.EqualTo(userEmail));
        }
    }
}
