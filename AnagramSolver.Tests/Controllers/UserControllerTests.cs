using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Tests.Helpers;
using AnagramSolver.WebApp.Controllers;
using NSubstitute;
using NUnit.Framework;

namespace AnagramSolver.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private IUserService _userService;
        private UserController _userController;
        private TestData _testWords;

        [SetUp]
        public void Setup()
        {
            _userService = Substitute.For<IUserService>();
            _userController = new UserController(_userService);
            _testWords = new TestData();
        }
        [Test]
        public void Should_CreateUser_When_GivenUserDto()
        {
            var userDto = _testWords.GetUserDto();

            var result = _userController.Create(userDto);

            Assert.IsNotNull(result.Result);
        }
    }
}
