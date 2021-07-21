using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class SearchLogWithSql : ISearchLog
    {
        private readonly string _connectionString = @"Data Source=LT-LIT-SC-0597\MSSQLSERVER01;Initial Catalog=VocabularyDB;Integrated Security=True";
        public void updateSearchLog(int elapsedTime, string wordForAnagrams, List<string> listOfAnagrams)
        {
            var createdAt = DateTime.Now;
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            string query = "INSERT INTO SearchLog (UserIp, Word, Anagrams, SearchTime, CreatedAt) VALUES (@ip, @word, @anagrams, @searchTime, @createdAt)";
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@ip", "1.1.1.1");
            cmd.Parameters.AddWithValue("@word", wordForAnagrams);
            cmd.Parameters.AddWithValue("@anagrams", listOfAnagrams.Count);
            cmd.Parameters.AddWithValue("@searchTime", elapsedTime);
            cmd.Parameters.AddWithValue("@createdAt", createdAt);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
