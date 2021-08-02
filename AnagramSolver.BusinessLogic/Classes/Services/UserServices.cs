using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Classes.Users
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> CreateUser(User user)
        {

            var hash = new PasswordService();
            var password = hash.GetHashString(user.Pass);
            var userToAdd = new User { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Pass = password };
            _userRepository.Insert(userToAdd);
            return true;
        }
        public void AddFavouriteWords(string email, string favouriteWords)
        {
            var allWords = favouriteWords.Split(" ");
            foreach (var word in allWords)
            {
                var userWord = new UserWord();
                var wordFromDb = _userRepository.GetWord(word);
                var userFromDb = _userRepository.GetByEmail(email);
                userWord.WordId = wordFromDb.Id;
                userWord.UserId = userFromDb.Id;
                _userRepository.AddUserWord(userWord);
            }
        }
        public string ReadUser(int id)
        {
            var userFromDb = _userRepository.GetById(id);
            var stringToShow = $"{userFromDb.FirstName}  {userFromDb.LastName}  {userFromDb.Email}";
            return stringToShow;
        }
        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

    }
}

