using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class CacheAnagram : ICacheAnagram
    {
        public CacheModel GetCachedAnagram(string command)
        {
            var cachedAnagrams = new CacheModel();
            var appSettings = ConfigurationManager.AppSettings;
            string connString = appSettings["ConnectionString"] ?? "Not Found";
            SqlConnection con = new(connString);
            string query = "select * from CachedWord";
            SqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = query;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (rdr["Word"].ToString().Contains(command))
                    {
                        var list = new List<string>();
                        list.Add(rdr["Anagram"].ToString());
                        cachedAnagrams.Caches = list;
                        cachedAnagrams.IsSuccessful = true;
                        return cachedAnagrams;
                    }
                }
            }
            con.Close();
            return cachedAnagrams;
        }

        public void PutAnagramToCache(string command, List<string> listOfAnagrams)
        {
            var anagramToDB = "";
            foreach (var anagram in listOfAnagrams)
            {
                anagramToDB += $"{anagram}  ";
            }

            var appSettings = ConfigurationManager.AppSettings;
            string connString = appSettings["ConnectionString"] ?? "Not Found";
            SqlConnection con = new(connString);
            con.Open();
            var cmd2 = new SqlCommand("INSERT INTO CachedWord (Word, Anagram) VALUES (@word, @anagram)", con);
            cmd2.Parameters.AddWithValue("@word", command);
            cmd2.Parameters.AddWithValue("@anagram", anagramToDB);
            cmd2.ExecuteNonQuery();

            con.Close();
        }
    }
}
