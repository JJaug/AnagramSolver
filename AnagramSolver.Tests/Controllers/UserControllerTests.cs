using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Controllers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private IUserService _userService;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _userService = Substitute.For<IUserService>();
            _userController = new UserController(_userService);
        }
        //[Test]
        //public void Creates
    }
}
