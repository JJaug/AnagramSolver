using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes.PersistentRepositories
{
    public class PersistentRepository : IPersistentRepository
    {
        private readonly IConfiguration config;
        public PersistentRepository(IConfiguration configuration)
        {
            config = configuration;
        }
        public void PopulateDataBaseFromFile()
        {
            var filePath = config.GetSection("MyConfig").GetSection("FilePath").Value;
            var connectionString = config.GetSection("MyConfig").GetSection("ConnectionString").Value;
            var minLength = int.Parse(config.GetSection("MyConfig").GetSection("MinWordLength").Value);


            HashSet<string> fileLines;
            fileLines = new HashSet<string>(File.ReadLines(filePath));
            var vocabulary = new HashSet<string>();

            foreach (string line in fileLines)
            {
                string[] wordsInLine = line.Split("\t").ToArray();
                if (wordsInLine[2].Length >= minLength)
                {
                    var word = wordsInLine[2];
                    vocabulary.Add(word);
                }
            }

            try
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();
                foreach (var word in vocabulary)
                {
                    var cmd = new SqlCommand("INSERT INTO Words (Word) VALUES (@word)", connection);
                    cmd.Parameters.AddWithValue("@word", word);
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
