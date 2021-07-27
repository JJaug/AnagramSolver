using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AnagramSolver.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration config;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            config = configuration;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserDto userDto)
        {
            //string _dbCon1 = config.GetSection("MyConfig").GetSection("ConnectionString").Value;
            _userService.CreateUser(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Pass, userDto.FavouriteWords);
            return View();
        }
    }
}
