﻿using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes.Users
{
    public class UserRepository : IUserRepository
    {
        private VocabularyDBContext _context;
        public UserRepository(VocabularyDBContext context)
        {
            _context = context;
        }
        public Word GetWord(string word)
        {
            var wordFromDB = _context.Words.FirstOrDefault(w => w.Word1 == word);
            return wordFromDB;


        }
        public long AddUser(string firstName, string lastName, string email, string password)
        {
            var userToAdd = new User { FirstName = firstName, LastName = lastName, Email = email, Pass = password };
            _context.Users.Add(userToAdd);
            _context.SaveChanges();
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user.Id;
        }
        public void AddUserWord(UserWord userWord)
        {

            _context.UserWords.Add(userWord);
            _context.SaveChanges();

        }
        public void AddUserWords(List<UserWord> userWords)
        {

            _context.UserWords.AddRange(userWords);
            _context.SaveChanges();

        }
        public User GetUser(long id)
        {

            var result = _context.Users.FirstOrDefault(u => u.Id == id);
            return result;

        }
        public void RemoveUser(long id)
        {

            var wordToRemove = _context.Users.FirstOrDefault(u => u.Id == id);
            _context.Users.Remove(wordToRemove);
            _context.SaveChanges();

        }
    }
}
