using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IUserService
    {
        public Task<bool> CreateUser(string firstName, string lastName, string email, string pass, string favouriteWords);
        public Task<string> ReadUser(long id);
        public void DeleteUser(long id);


    }
}
