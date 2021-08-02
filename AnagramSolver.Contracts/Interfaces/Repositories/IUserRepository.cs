using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<Word> GetWord(string word);
        public void AddUserWord(UserWord userWord);
        public User GetByEmail(string email);
    }
}
