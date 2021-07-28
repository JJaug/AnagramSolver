using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes.Repositories
{
    public class FileToDatabaseRepository : IFileToDatabaseRepository
    {
        private readonly VocabularyDBContext _context;
        public FileToDatabaseRepository(VocabularyDBContext context)
        {
            _context = context;
        }
        public void AddWordsToDatabase(HashSet<string> vocabulary)
        {
            try
            {
                foreach (var word in vocabulary)
                {
                    var wordToAdd = new Word { Word1 = word };
                    _context.Words.Add(wordToAdd);
                    if (_context.Words.Count() % 30000 == 0)
                        _context.SaveChanges();
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
