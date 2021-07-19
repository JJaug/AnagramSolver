using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System;
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
        private string connString;

        public string Init()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string connString = appSettings["ConnectionString"] ?? "Not Found";
            return connString;
        }
        public HashSet<WordModel> GetAllWords()
        {
            connString = Init();
            SqlConnection con = new SqlConnection(connString);
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
            connString = Init();
            SqlConnection con = new SqlConnection(connString);
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

            return wordsFromDB.Skip(howManySkip).Take(wordsInPage).ToHashSet();
        }

        public HashSet<string> GetSpecificWords(string word)
        {
            var neededWords = new HashSet<string>();
            connString = Init();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM Words WHERE Word LIKE '%{word}%'";
            var rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    neededWords.Add(rdr["Word"].ToString());
                }
                
            }
            con.Close();
            return neededWords;
        }
    }
}
