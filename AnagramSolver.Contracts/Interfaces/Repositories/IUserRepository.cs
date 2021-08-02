using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<Word> GetWord(string word);
        public Task<long> AddUser(string firstName, string lastName, string email, string password);
        public void AddUserWord(UserWord userWord);
        public Task<bool> AddUserWords(List<UserWord> userWords);
        public User GetByEmail(string email);



    }
}
