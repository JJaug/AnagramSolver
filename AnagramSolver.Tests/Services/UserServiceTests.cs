using AnagramSolver.BusinessLogic.Classes.Users;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Tests.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace AnagramSolver.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserRepository _userRepository;
        private IUserService _userService;
        private TestData _testWords;

        [SetUp]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _userService = new UserServices(_userRepository);
            _testWords = new TestData();
        }
        [Test]
        public void Should_ReadUser_When_GivenUserID()
        {
            var testUser = _testWords.CreateTestUser();
            _userRepository.GetById(Arg.Any<int>()).Returns(testUser);
            var stringToShow = $"{testUser.FirstName}  {testUser.LastName}  {testUser.Email}";

            var result = _userService.ReadUser((int)testUser.Id);

            Assert.AreEqual(stringToShow, result);
        }
        [Test]
        public void Should_CreateUser_When_GivenUserInfo()
        {
            var testUser = _testWords.CreateTestUser();
            _userRepository.Insert(testUser);

            var isSuccessful = _userService.CreateUser(testUser);

            Assert.IsTrue(isSuccessful.Result);
        }

    }
}
