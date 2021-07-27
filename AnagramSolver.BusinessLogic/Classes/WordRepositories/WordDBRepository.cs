using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes.WordRepositories
{

    public class WordDBRepository : IWordRepository
    {
        private readonly IConfiguration config;
        private int wordsInPage;
        private string connectionString;
        public WordDBRepository(IConfiguration configuration)
        {
            config = configuration;
        }
        public HashSet<WordModel> GetAllWords()
        {
            connectionString = config.GetSection("MyConfig").GetSection("ConnectionString").Value;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Words";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            var wordsFromDB = new HashSet<WordModel>();

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {

                    var word = new WordModel();
                    word.Word = rdr["Word"].ToString();
                    word.ID = (int)rdr["ID"];
                    wordsFromDB.Add(word);

                }
            }
            con.Close();

            return wordsFromDB;
        }

        public HashSet<WordModel> GetSpecificPage(int pageNumber)
        {
            wordsInPage = int.Parse(config.GetSection("MyConfig").GetSection("WordsInPage").Value);
            var howManySkip = (pageNumber * wordsInPage) - wordsInPage;
            connectionString = config.GetSection("MyConfig").GetSection("ConnectionString").Value;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Words";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            var wordsFromDB = new HashSet<WordModel>();

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {

                    var word = new WordModel();
                    word.Word = rdr["Word"].ToString();
                    word.ID = (long)rdr["ID"];
                    wordsFromDB.Add(word);

                }
            }
            con.Close();

            return wordsFromDB.Skip(howManySkip).Take(wordsInPage).ToHashSet();
        }

        public HashSet<string> GetSpecificWords(string word)
        {
            var specificWords = new HashSet<string>();
            connectionString = config.GetSection("MyConfig").GetSection("ConnectionString").Value;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM Words WHERE Word LIKE '%{word}%'";
            var rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    specificWords.Add(rdr["Word"].ToString());
                }

            }
            con.Close();
            return specificWords;
        }
    }
}
