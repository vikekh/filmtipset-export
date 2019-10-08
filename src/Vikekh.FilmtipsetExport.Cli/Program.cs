using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.Hosting.CommandLine;
using Vikekh.FilmtipsetExport.Cli.Interfaces;
using Vikekh.FilmtipsetExport.Cli.Services;
using Microsoft.Extensions.Logging;
using Vikekh.FilmtipsetExport.Cli.Models;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;

namespace Vikekh.FilmtipsetExport.Cli
{
    public class Program
    {
        private readonly ILogger<Program> _logger;
        private readonly IMovieService _movieService;
        private readonly IExportService _exportService;

        public string File { get; } = @"C:\Users\Viktor\.ftexp\Movies.json";

        public string Output { get; } = @"Movies.csv";

        public Program(ILogger<Program> logger, IMovieService movieService, IExportService exportService)
        {
            _logger = logger;
            _movieService = movieService;
            _exportService = exportService;

            _logger.LogInformation("Constructed!");
        }

        public static async Task<int> Main(string[] args)
        {
            return await new HostBuilder()
                .ConfigureLogging((context, builder) =>
                {
                    //builder.AddConsole();
                })
                .ConfigureServices((context, services) => {
                    services.AddHttpClient<IScraperService, ScraperService>();
                    services.AddTransient<IMovieService, MovieService>()
                        .AddTransient<IExportService, ExportService>()
                        .AddSingleton<IConsole>(PhysicalConsole.Singleton);
                })
                .RunCommandLineApplicationAsync<Program>(args);
        }

        private void OnExecute()
        {
            _movieService.Init(File);
            var movies = _movieService.GetList();
            var export = new List<string>();
            export.Add("imdbID,Rating,WatchedDate");
            export.AddRange(movies.Where(movie => movie.ImdbId != null)
                .Select(movie => $"{movie.ImdbId},{movie.Grade},{movie.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}")
            );

            var unmatched = movies.Where(movie => movie.ImdbId == null);
            Console.WriteLine("Unmatched:");
            var i = 1;
            foreach (var movie in unmatched)
            {
                Console.WriteLine($"{ i.ToString("0000")}. Title: {movie.Title} Path: {movie.Url} Grade: {movie.Grade} Date: {movie.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}");
                i++;
            }

            _exportService.WriteCsv(export, Output);

            // _movieService.Update("vieekk");
            // _movieService.Save(File);

            // switch (Command)
            // {
            //     case "Export":
            //     case "Update":
            // }

            //var movies = new Dictionary<string, Movie>();

            //using (var file = new StreamReader(@"C:\Users\Viktor\.ftexp\Movies.json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    movies = (Dictionary<string, Movie>)serializer.Deserialize(file, typeof(IDictionary<string, Movie>));
            //}

            //for (var i = 0; i <= 37; i++)
            //{
            //    foreach (var movie in _filmtipsetService.GetMovieGradesAsync("vieekk", i).Result)
            //    {
            //        if (!movies.TryAdd(movie.Url, movie))
            //        {
            //            Console.Write($"duplicate {movie.Url}: {movie.Title}, grade {movie.Grade}, date {movie.Date.ToLongDateString()}");
            //        }
            //    }

            //    Console.WriteLine($"Count: {movies.Count}");
            //}


            //var i = 0;

            //foreach (var movie in movies.Where(x => x.Value.ImdbId == null))
            //{
            //    if (i % 10 == 0)
            //    {
            //        Console.WriteLine("Wait 10 s");
            //        System.Threading.Thread.Sleep(10000);
            //    }

            //    //if (i == 20) break;

            //    if (movie.Value.ImdbId != null)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        i++;
            //    }

            //    try
            //    {
            //        var id = _filmtipsetService.GetMovieAsync(movie.Key).Result;

            //        if (id == null)
            //        {
            //            Console.Write($"null id {movie.Value.Url}: {movie.Value.Title}");
            //        }

            //        movie.Value.ImdbId = id;
            //    }
            //    catch {
            //        Console.Write($"HTTP error {movie.Value.Url}: {movie.Value.Title}");
            //    }
                

            //}

            //// serialize JSON directly to a file
            //using (StreamWriter file = File.CreateText(@"C:\Users\Viktor\movies.json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(file, movies);
            //}

            //Console.WriteLine("Bye bye");
        }
    }
}
