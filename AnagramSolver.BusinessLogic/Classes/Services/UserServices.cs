using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic.Classes.Users
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void CreateUser(string firstName, string lastName, string email, string pass, string favouriteWords)
        {

            var hash = new PasswordService();
            var password = hash.GetHashString(pass);
            var allWords = favouriteWords.Split(" ");
            var id = _userRepository.AddUser(firstName, lastName, email, password);
            var userWords = new List<UserWord>();

            foreach (var word in allWords)
            {
                var userWord = new UserWord();
                var wordFromDb = _userRepository.GetWord(word);
                userWord.WordId = wordFromDb.Id;
                userWord.UserId = id;
                userWords.Add(userWord);
            }
            _userRepository.AddUserWords(userWords);
        }
        public string ReadUser(long id)
        {
            var userFromDb = _userRepository.GetUser(id);
            var stringToShow = $"{userFromDb.FirstName}  {userFromDb.LastName}  {userFromDb.Email}";
            return stringToShow;
        }
        public void DeleteUser(long id)
        {
            _userRepository.RemoveUser(id);
        }

    }
}

