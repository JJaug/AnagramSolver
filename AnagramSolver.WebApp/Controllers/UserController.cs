﻿using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create(UserDto userDto)
        {
            _userService.CreateUser(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Pass, userDto.FavouriteWords);
            return View();
        }
    }
}
