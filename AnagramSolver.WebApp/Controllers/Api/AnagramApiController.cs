﻿using AnagramSolver.BusinessLogic.Classes;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace AnagramSolver.WebApp.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class AnagramApiController : ControllerBase
    {
        private readonly ILogger<AnagramApiController> _logger;
        private readonly string _filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data/zodynas.txt";
        private readonly string connectionString = @"Data Source=LT-LIT-SC-0597\MSSQLSERVER01;Initial Catalog=VocabularyDB;Integrated Security=True";
        private readonly IWordRepository _wordRepository;
        public AnagramApiController(ILogger<AnagramApiController> logger)
        {
            _logger = logger;
            _wordRepository = new WordDBRepository();

        }
        [HttpGet("[action]/{id}")]
        public string GetJsonString(string id)
        {
            var vocabularyByModel = new HashSet<AnagramViewModel>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var allAnagramById = _anagramSolver.GetAnagrams(id);
            foreach (var word in allAnagramById)
            {
                var anagram = new AnagramViewModel();
                anagram.AnagramWord = word;
                anagram.Word = id;
                vocabularyByModel.Add(anagram);
            }
            string jsonString = JsonSerializer.Serialize(vocabularyByModel);
            return jsonString;
        }
        [HttpGet("[action]/{id}")]
        public List<string> GetForJS(string id)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var numberOfAnagrams = 0;
            var listOfAnagrams = new List<string>();
            var allWords = _wordRepository.GetAllWords();
            var _anagramSolver = new BusinessLogic.Classes.AnagramSolver(allWords);
            var anagramsById = _anagramSolver.GetAnagrams(id);
            foreach (var word in anagramsById)
            {
                numberOfAnagrams++;
                listOfAnagrams.Add(word);
            }
            stopWatch.Stop();

            var createdAt = DateTime.Now;
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00} s,{1:00} ms",
            ts.Seconds, ts.Milliseconds / 10);
            var httpClient = new HttpClient();
            var ip = httpClient.GetStringAsync("https://api.ipify.org");
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "INSERT INTO SearchLog (UserIp, Word, Anagrams, SearchTime, CreatedAt) VALUES (@ip, @word, @anagrams, @searchTime, @createdAt)";
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@ip", "1");
            cmd.Parameters.AddWithValue("@word", id); 
            cmd.Parameters.AddWithValue("@anagrams", numberOfAnagrams);
            cmd.Parameters.AddWithValue("@searchTime", elapsedTime);
            cmd.Parameters.AddWithValue("@createdAt", createdAt);
            cmd.ExecuteNonQuery();
            con.Close();

            return listOfAnagrams;
        }
        [HttpGet("[action]/{word}")]
        public HashSet<string> GetSearchList(string wordPart)
        {
            var wordsContainingSpecificPart = _wordRepository.GetSpecificWords(wordPart);

            return wordsContainingSpecificPart;
        }
    }
}
