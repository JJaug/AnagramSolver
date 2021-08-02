using AnagramSolver.EF.DatabaseFirst.Models;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IUserService
    {
        public Task<bool> CreateUser(User user);


    }
}
