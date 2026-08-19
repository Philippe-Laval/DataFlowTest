using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace DataFlow.Library;

/// <summary>
/// Demonstrates how to create a basic dataflow pipeline.
/// This program downloads the book "The Iliad of Homer" by Homer from the Web
/// and finds all reversed words that appear in that book.
/// </summary>
public static class DataflowReversedWords
{
    public static void Run()
    {
        //
        // Create the members of the pipeline.
        //

        // Downloads the requested resource as a string.
        var downloadString = new TransformBlock<string, string>(async uri =>
        {
            Console.WriteLine($"Downloading '{uri}'...");

            var result = string.Empty;

            try
            {
                var httpClient = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
                result = await httpClient.GetStringAsync(uri);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Exception Downloading '{uri}': {ex.Message}");
            }
            return result;
        });

        // Separates the specified text into an array of words.
        var createWordList = new TransformBlock<string, string[]>(text =>
        {
            Console.WriteLine("Creating word list...");

            try
            {
                // Remove common punctuation by replacing all non-letter characters
                // with a space character.
                char[] tokens = text.Select(c => char.IsLetter(c) ? c : ' ').ToArray();
                text = new string(tokens);

                // Separate the text into an array of words.
                return text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Creating word list: {ex.Message}");
            }

            return new string[0];
        });

        // Removes short words and duplicates.
        var filterWordList = new TransformBlock<string[], string[]>(words =>
        {
            Console.WriteLine("Filtering word list...");
            try
            {
                return words
                   .Where(word => word.Length > 3)
                   .Distinct()
                   .ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Filtering word list: {ex.Message}");
            }

            return new string[0];
        });

        // Finds all words in the specified collection whose reverse also
        // exists in the collection.
        var findReversedWords = new TransformManyBlock<string[], string>(words =>
        {
            // Since we use TransformManyBlock, this block will call several time the next block
            // with each word found a the parallel query

            Console.WriteLine("Finding reversed words...");

            try
            {
                var wordsSet = new HashSet<string>(words);

                return from word in words.AsParallel()
                       let reverse = new string(word.Reverse().ToArray())
                       where word != reverse && wordsSet.Contains(reverse)
                       select word;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Finding reversed words: {ex.Message}");
            }

            return new string[0];
        });

        // Prints the provided reversed words to the console.
        var printReversedWords = new ActionBlock<string>(reversedWord =>
        {
            Console.WriteLine($"Found reversed words {reversedWord}/{new string(reversedWord.Reverse().ToArray())}");
        });

        //
        // Connect the dataflow blocks to form a pipeline.
        //

        var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

        downloadString.LinkTo(createWordList, linkOptions);
        createWordList.LinkTo(filterWordList, linkOptions);
        filterWordList.LinkTo(findReversedWords, linkOptions);
        findReversedWords.LinkTo(printReversedWords, linkOptions);

        // Process "The Iliad of Homer" by Homer.
        downloadString.Post("http://www.gutenberg.org/cache/epub/16452/pg16452.txt");

        // Mark the head of the pipeline as complete.
        downloadString.Complete();

        // Wait for the last block in the pipeline to process all messages.
        printReversedWords.Completion.Wait();
    }
}
