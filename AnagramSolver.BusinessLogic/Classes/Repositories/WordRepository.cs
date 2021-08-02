using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes.WordRepositories
{
    public class WordRepository : IWordRepository
    {
        private readonly VocabularyDBContext _context;
        public WordRepository(VocabularyDBContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Words.Remove(_context.Words.FirstOrDefault(w => w.Id == id));
            Save();
        }

        public IEnumerable<Word> GetAll()
        {
            var allWords = _context.Words.ToList();
            return allWords;
        }

        public Word GetById(int id)
        {
            var wordFromDb = _context.Words
            .FirstOrDefault(w => w.Id.Equals(id));
            return wordFromDb;
        }
        public void Insert(Word obj)
        {
            _context.Words.Add(obj);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Word obj)
        {
            _context.Words.Remove(_context.Words.FirstOrDefault(w => w.Id == obj.Id));
            _context.Words.Add(obj);
            Save();
        }
        public HashSet<Word> GetSpecificWords(string wordPart)
        {
            var wordsFromDb = _context.Words
                .Where(w => w.Word1.Contains(wordPart))
                .ToHashSet();

            return wordsFromDb;
        }
    }
}
