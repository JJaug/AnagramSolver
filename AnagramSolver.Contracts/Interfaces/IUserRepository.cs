using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IUserRepository
    {
        public Word GetWord(string word);
        public long CreateUser(string firstName, string lastName, string email, string password);
        public void AddUserWord(UserWord userWord);
        public void AddUserWords(List<UserWord> userWords);

    }
}
