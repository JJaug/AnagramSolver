using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Classes.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly VocabularyDBContext _context;
        public UserRepository(VocabularyDBContext context)
        {
            _context = context;
        }
        public Task<Word> GetWord(string word)
        {
            var wordFromDB = _context.Words.FirstOrDefault(w => w.Word1 == word);
            return Task.FromResult(wordFromDB);
        }
        public User GetByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }
        public async void AddUserWord(UserWord userWord)
        {
            await _context.UserWords.AddAsync(userWord);
            _context.SaveChanges();
        }
        public IEnumerable<User> GetAll()
        {
            var allUsers = _context.Users.ToList();
            return allUsers;
        }

        public User GetById(int id)
        {
            var userFromDb = _context.Users.FirstOrDefault(w => w.Id == id);
            return userFromDb;
        }

        public void Insert(User obj)
        {
            _context.Users.Add(obj);
            Save();
        }

        public void Update(User obj)
        {
            _context.Users.Remove(_context.Users.FirstOrDefault(w => w.Id == obj.Id));
            _context.Users.Add(obj);
            Save();
        }

        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.FirstOrDefault(w => w.Id == id));
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
