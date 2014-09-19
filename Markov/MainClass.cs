// <copyright file="MainClass.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace Markov
{
        using System;
        using System.IO;
        using MarkovProcess;

        /// <summary>
        /// Main class.
        /// </summary>
        public class MainClass
        {
                /// <summary>
                /// The entry point of the program, where the program control starts and ends.
                /// </summary>
                /// <param name="args">The command-line arguments.</param>
                public static void Main(string[] args)
                {
                        var mkv = new Markov<char>(3);
                        StreamReader input = File.OpenText("file.txt");
                        int next;
                        char c;

                        while (!input.EndOfStream)
                        {
                                next = input.Read();
                                c = Convert.ToChar(next);
                                if (char.IsWhiteSpace(c))
                                {
                                        mkv.ClearTraining();
                                }
                                else
                                {
                                        mkv.AddNext(char.ToLower(c));
                                }
                        }

                        for (int i = 0; i < 1000; i++)
                        {
                                c = mkv.Generate();
                                if (c == default(char))
                                {
                                        c = ' ';
                                }

                                Console.Write(c);
                        }

                        Console.WriteLine();
                }
        }
}