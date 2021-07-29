using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Classes.Users
{
    public class UserRepository : IUserRepository
    {
        private VocabularyDBContext _context;
        public UserRepository(VocabularyDBContext context)
        {
            _context = context;
        }
        public Task<Word> GetWord(string word)
        {
            var wordFromDB = _context.Words.FirstOrDefault(w => w.Word1 == word);
            return Task.FromResult(wordFromDB);


        }
        public Task<long> AddUser(string firstName, string lastName, string email, string password)
        {
            var userToAdd = new User { FirstName = firstName, LastName = lastName, Email = email, Pass = password };
            _context.Users.Add(userToAdd);
            _context.SaveChanges();
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return Task.FromResult(user.Id);
        }
        public async void AddUserWord(UserWord userWord)
        {

            await _context.UserWords.AddAsync(userWord);
            _context.SaveChanges();

        }
        public Task<bool> AddUserWords(List<UserWord> userWords)
        {
            _context.UserWords.AddRange(userWords);
            _context.SaveChanges();
            return Task.FromResult(true);
        }
        public Task<User> GetUser(long id)
        {

            var result = _context.Users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(result);

        }
        public async void RemoveUser(long id)
        {

            var wordToRemove = _context.Users.FirstOrDefault(u => u.Id == id);
            _context.Users.Remove(wordToRemove);
            await _context.SaveChangesAsync();

        }
    }
}
