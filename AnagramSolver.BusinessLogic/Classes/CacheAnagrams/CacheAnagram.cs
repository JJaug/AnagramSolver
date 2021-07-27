﻿using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnagramSolver.BusinessLogic.Classes.CacheAnagrams
{
    public class CacheAnagram : ICacheAnagram
    {
        private readonly IConfiguration config;
        public CacheAnagram(IConfiguration configuration)
        {
            config = configuration;
        }
        public CacheModel GetCachedAnagram(string command)
        {
            var cachedAnagrams = new CacheModel();
            string connString = config.GetSection("MyConfig").GetSection("ConnectionString").Value;
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
                        var list = new HashSet<AnagramModel>();
                        var anagramModel = new AnagramModel();
                        anagramModel.Word = command;
                        anagramModel.AnagramWord = rdr["Anagram"].ToString();
                        list.Add(anagramModel);
                        cachedAnagrams.Caches = list;
                        cachedAnagrams.IsSuccessful = true;
                        return cachedAnagrams;
                    }
                }
            }
            con.Close();
            return cachedAnagrams;
        }

        public void PutAnagramToCache(string command, HashSet<AnagramModel> listOfAnagrams)
        {
            var anagramToDB = string.Empty;
            foreach (var anagram in listOfAnagrams)
            {
                anagramToDB += $"{anagram.AnagramWord}  ";
            }
            string connString = config.GetSection("MyConfig").GetSection("ConnectionString").Value;
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
