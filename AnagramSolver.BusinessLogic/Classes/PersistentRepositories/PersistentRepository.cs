using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes.PersistentRepositories
{
    public class PersistentRepository : IPersistentRepository
    {
        public void PopulateDataBaseFromFile()
        {
            var filePath = ReadSetting("FilePath");
            var connectionString = ReadSetting("ConnectionString");
            var minLength = int.Parse(ReadSetting("MinWordLength"));


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
        public string ReadSetting(string key)
        {

            var appSettings = ConfigurationManager.AppSettings;
            string specificSetting = appSettings[key] ?? "Not Found";
            return specificSetting;

        }
    }
}
