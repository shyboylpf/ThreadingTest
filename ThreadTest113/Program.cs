using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest113
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parallel.ForEach("Hello, world", (c, state, i) =>
            {
                Console.WriteLine(c.ToString() + i);
            });

            if (!File.Exists("WordLookup.txt"))    // Contains about 150,000 words
                new WebClient().DownloadFile(
                  "http://www.albahari.com/ispell/allwords.txt", "WordLookup.txt");

            var wordLookup = new HashSet<string>(
              File.ReadAllLines("WordLookup.txt"),
              StringComparer.InvariantCultureIgnoreCase);

            var random = new Random();
            string[] wordList = wordLookup.ToArray();

            string[] wordsToTest = Enumerable.Range(0, 1000000)
              .Select(i => wordList[random.Next(0, wordList.Length)])
              .ToArray();

            wordsToTest[12345] = "woozsh";     // Introduce a couple
            wordsToTest[23456] = "wubsie";     // of spelling mistakes.

            var misspellings = new ConcurrentBag<Tuple<int, string>>();
            Parallel.ForEach(wordsToTest, (word, state, i) =>
            {
                if (!wordLookup.Contains(word))
                    misspellings.Add(Tuple.Create((int)i, word));
            });

            foreach (var item in misspellings)
            {
                Console.WriteLine($"{item.Item1}: {item.Item2}");
            }
        }
    }
}