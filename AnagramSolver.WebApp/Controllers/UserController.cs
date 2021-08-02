using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            var userToAdd = new User { FirstName = userDto.FirstName, LastName = userDto.LastName, Email = userDto.Email, Pass = userDto.Pass };
            var favouriteWords = userDto.FavouriteWords;
            return View();
        }
    }
}
