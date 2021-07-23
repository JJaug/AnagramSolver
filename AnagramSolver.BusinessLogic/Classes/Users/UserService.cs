using AnagramSolver.EF.DatabaseFirst.Models;

namespace AnagramSolver.BusinessLogic.Classes.Users
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService()
        {
            _userRepository = new UserRepository();
        }
        public void CreateUser(long id, string name, string lastName, string email, string pass, string favouriteWords)
        {

            var hash = new PasswordService();
            var password = hash.GetHashString(pass);
            var allWords = favouriteWords.Split(" ");
            var userWord = new UserWord();



            foreach (var word in allWords)
            {
                var wordFromDb = _userRepository.ConnectWithDb(word);
                userWord.WordId = wordFromDb.Id;
                userWord.UserId = id;
            }
        }

    }
}

