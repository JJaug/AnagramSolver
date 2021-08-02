using AnagramSolver.EF.DatabaseFirst.Models;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IUserService
    {
        public Task<bool> CreateUser(User user);
        public void AddFavouriteWords(string email, string favouriteWords);
        public string ReadUser(int id);
        public void DeleteUser(int id);




    }
}
