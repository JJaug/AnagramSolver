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
        public void CreateUser(string firstName, string lastName, string email, string pass, string favouriteWords)
        {

            var hash = new PasswordService();
            var password = hash.GetHashString(pass);
            var allWords = favouriteWords.Split(" ");
            var id = _userRepository.UploadToDb(firstName, lastName, email, password);


            foreach (var word in allWords)
            {
                var userWord = new UserWord();
                var wordFromDb = _userRepository.ConnectWithDb(word);
                userWord.WordId = wordFromDb.Id;
                userWord.UserId = id;
                _userRepository.AddUserWord(userWord);
            }
        }

    }
}

