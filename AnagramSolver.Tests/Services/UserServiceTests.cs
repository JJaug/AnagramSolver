using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.BusinessLogic.Classes.Users;
using AnagramSolver.WebApp.Models;
using AnagramSolver.EF.DatabaseFirst.Models;

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
            _userRepository.GetUser(userId).Returns(user);
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
            var wordFromDb = new Word { Id = 1, Word1 = "neprisikiskiakopusteliaudavome" };
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
