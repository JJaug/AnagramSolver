using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;

namespace AnagramSolver.BusinessLogic.Classes.Users
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService()
        {
            _userRepository = new UserRepository();
        }
        public void CreateUser(string firstName, string lastName, string email, string pass, string favouriteWords)
        {

            var hash = new PasswordService();
            var password = hash.GetHashString(pass);
            var allWords = favouriteWords.Split(" ");
            var id = _userRepository.CreateUser(firstName, lastName, email, password);
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

    }
}

