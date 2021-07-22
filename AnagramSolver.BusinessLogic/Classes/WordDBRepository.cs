using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{

    public class WordDBRepository : IWordRepository
    {
        private const int wordsInPage = 100;
        private const int _minLength = 4;
        private string connectionString;

        public string Init()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string connectionString = appSettings["ConnectionString"] ?? "Not Found";
            return connectionString;
        }
        public HashSet<WordModel> GetAllWords()
        {
            connectionString = Init();
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
                    if (rdr["Word"].ToString().Length >= _minLength)
                    {
                        var word = new WordModel();
                        word.Word = rdr["Word"].ToString();
                        word.ID = (int)rdr["ID"];
                        wordsFromDB.Add(word);
                    }
                }
            }
            con.Close();

            return wordsFromDB;
        }

        public HashSet<WordModel> GetSpecificPage(int pageNumber)
        {
            var howManySkip = (pageNumber * wordsInPage) - wordsInPage;
            connectionString = Init();
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
                    if (rdr["Word"].ToString().Length >= _minLength)
                    {
                        var word = new WordModel();
                        word.Word = rdr["Word"].ToString();
                        word.ID = (long)rdr["ID"];
                        wordsFromDB.Add(word);
                    }
                }
            }
            con.Close();

            return wordsFromDB.Skip(howManySkip).Take(wordsInPage).ToHashSet();
        }

        public HashSet<string> GetSpecificWords(string word)
        {
            var specificWords = new HashSet<string>();
            connectionString = Init();
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
