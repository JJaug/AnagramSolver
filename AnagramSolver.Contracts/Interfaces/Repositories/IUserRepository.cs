using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IUserRepository
    {
        public Task<Word> GetWord(string word);
        public Task<long> AddUser(string firstName, string lastName, string email, string password);
        public void AddUserWord(UserWord userWord);
        public Task<bool> AddUserWords(List<UserWord> userWords);
        public Task<User> GetUser(long id);
        public void RemoveUser(long id);


    }
}
