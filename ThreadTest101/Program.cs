﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest101
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (!File.Exists("WordLookup.txt"))
            {
                new WebClient().DownloadFile("http://www.albahari.com/ispell/allwords.txt", "WordLookup.txt");
            }

            var wordLookup = new HashSet<string>(
                File.ReadAllLines("WordLookup.txt"),
                StringComparer.InvariantCultureIgnoreCase);

            //var random = new Random();
            var localRandom = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
            string[] wordList = wordLookup.ToArray();

            string[] wordsToTest = Enumerable.Range(0, 1000000)
                .AsParallel()
                .Select(i => wordList[localRandom.Value.Next(0, wordList.Length)])
                .ToArray();

            wordsToTest[12345] = "woozsh";
            wordsToTest[23456] = "wubsie";

            var query = wordsToTest
                .AsParallel()
                .Select((word, index) => new IndexedWord { Word = word, Index = index })
                .Where(iword => !wordLookup.Contains(iword.Word))
                .OrderBy(iword => iword.Index);

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Index}|{item.Word}");
            }
        }
    }

    internal struct IndexedWord
    { public string Word; public int Index; }
}