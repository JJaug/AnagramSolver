using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Classes.Repositories
{
    public class FileToDatabaseRepository : IFileToDatabaseRepository
    {
        private readonly VocabularyDBContext _context;
        public FileToDatabaseRepository(VocabularyDBContext context)
        {
            _context = context;
        }
        public async Task AddWordsToDatabase(HashSet<Word> vocabulary)
        {
            try
            {
                await _context.Words.AddRangeAsync(vocabulary).ConfigureAwait(false);

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
