using AnagramSolver.Contracts.Interfaces;
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
            await _userService.CreateUser(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Pass, userDto.FavouriteWords);
            return View();
        }
    }
}
