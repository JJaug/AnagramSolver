using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes.Users
{
    public class UserRepository : IUserRepository
    {
        public Word GetWord(string word)
        {
            using (var context = new VocabularyDBContext())
            {
                var wordFromDB = context.Words.FirstOrDefault(w => w.Word1 == word);
                return wordFromDB;
            }

        }
        public long AddUser(string firstName, string lastName, string email, string password)
        {
            using (var context = new VocabularyDBContext())
            {
                var userToAdd = new User { FirstName = firstName, LastName = lastName, Email = email, Pass = password };
                context.Users.Add(userToAdd);
                context.SaveChanges();
                var user = context.Users.FirstOrDefault(u => u.Email == email);
                return user.Id;
            }
        }
        public void AddUserWord(UserWord userWord)
        {
            using (var context = new VocabularyDBContext())
            {
                context.UserWords.Add(userWord);
                context.SaveChanges();
            }
        }
        public void AddUserWords(List<UserWord> userWords)
        {
            using (var context = new VocabularyDBContext())
            {
                context.UserWords.AddRange(userWords);
                context.SaveChanges();
            }
        }
        public User GetUser(long id)
        {
            using (var context = new VocabularyDBContext())
            {
                var result = context.Users.FirstOrDefault(u => u.Id == id);
                return result;
            }
        }
        public void RemoveUser(long id)
        {
            using (var context = new VocabularyDBContext())
            {
                var wordToRemove = context.Users.FirstOrDefault(u => u.Id == id);
                context.Users.Remove(wordToRemove);
                context.SaveChanges();
            }
        }
    }
}
