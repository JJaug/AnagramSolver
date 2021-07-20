using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Models.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Classes
{
    public class AnagramSolver : IAnagramSolver
    {

        private readonly HashSet<WordModel> _allWords;
        public AnagramSolver(HashSet<WordModel> allWords)
        {
            _allWords = allWords;
        }
        public List<string> GetAnagrams(string command)
        {
            var newList = new List<string>();
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
                        newList.Add(rdr["Anagram"].ToString());
                        return newList;
                    }
                }
            }
            con.Close();
            var anagramToDB = "";
            char[] commandChars = command.ToCharArray();
            Array.Sort(commandChars);
            foreach (var word in _allWords)
            {
                if (word.Word.Length == command.Length)
                {
                    char[] wordChars = word.Word.ToCharArray();
                    Array.Sort(wordChars);
                    bool isEqual = Enumerable.SequenceEqual(wordChars, commandChars);
                    if (isEqual)
                    {
                        if (word.Word != command)
                        {
                            anagramToDB += $"{word.Word}  ";
                            newList.Add(word.Word);
                        }
                    }

                }
            }
            con.Open();
            var cmd2 = new SqlCommand("INSERT INTO CachedWord (Word, Anagram) VALUES (@word, @anagram)", con);
            cmd2.Parameters.AddWithValue("@word", command);
            cmd2.Parameters.AddWithValue("@anagram", anagramToDB);
            cmd2.ExecuteNonQuery();

            con.Close();
            return newList;
        }
    }
}

//var listOfCommands = command.Split(" ");
//var newList = new List<Anagram>();
//var commandList = new List<Words>();

//for(int i = 0; i < listOfCommands.Length; i++)
//{
//    Words commandItem = new Words();
//    commandItem.Word = listOfCommands[i];
//    var items = allWords.Where(x => x == commandItem.Word).ToList();
//    var item = allWords.SingleOrDefault(x => x == commandItem.Word);
//    if (item != null)
//        allWords.Remove(item);
//}

//command = command.Replace(" ", string.Empty);

//var commandChars = command.ToList();
//var remainingChars = commandChars;
//var containsChars = remainingChars;
//var result = string.Empty;

//SentenceMaker(remainingChars);

//void SentenceMaker(List<char> remainingChars)
//{
//    var contains = false;
//    foreach (var combination in allWords)
//    {
//        var word = combination;

//        if(combination.Length >= minLength)
//        {
//            var wordChars = word.ToCharArray();
//            foreach (char charInWord in word)
//            {
//                if (containsChars.Contains(charInWord))
//                {
//                    containsChars.Remove(charInWord);
//                    contains = true;
//                }else
//                {
//                    contains = false;
//                    break;
//                }
//            }

//            if (contains)
//            {
//                foreach (char charInWord in wordChars)
//                {
//                    remainingChars.Remove(charInWord);
//                }
//                var item = allWords.SingleOrDefault(x => x == word);
//                if (item != null)
//                    allWords.Remove(item);
//                result += $"{word} ";
//                if (!remainingChars.Any())
//                {
//                    Anagram anagram = new Anagram();
//                    anagram.Word = result;
//                    newList.Add(anagram);
//                } else if(combination == allWords.Last())
//                {
//                    break;
//                }
//                SentenceMaker(remainingChars);
//            }
//        }

//    }
//}
