using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Classes.WordRepositories
{
    public class WordRepository : IWordRepository
    {
        private readonly VocabularyDBContext _context;
        public WordRepository(VocabularyDBContext context)
        {
            _context = context;
        }

        public Task<HashSet<Word>> GetSpecificWords(string wordPart)
        {
            var wordsFromDb = _context.Words
                .Where(w => w.Word1.Contains(wordPart))
                .ToHashSet();

            return Task.FromResult(wordsFromDb);
        }
        public Task<List<Word>> GetWords()
        {
            var allWords = _context.Words.ToList();
            return Task.FromResult(allWords);
        }
    }
}
