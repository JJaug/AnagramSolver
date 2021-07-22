
using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User model)
        {
            using (var context = new VocabularyCodeFirstContext())
            {
                var hash = new HashPass();
                var pass = hash.GetHashString(model.Pass);
                //var allWards = model.Words.First().UserWords.User;
                var userToAdd = new User { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, Pass = pass, Words = model.Words };
                context.Users.Add(userToAdd);

            }


            return View();
        }
    }
}
