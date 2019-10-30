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
        private readonly IScraperService _scraperService;

        public string File { get; } = @"C:\Users\Viktor\.ftexp\Movies.json";

        public string Output { get; } = @"Movies.csv";

        public Program(ILogger<Program> logger, IMovieService movieService, IExportService exportService/*, IScraperService scraperService*/)
        {
            _logger = logger;
            _movieService = movieService;
            _exportService = exportService;
            //_scraperService = scraperService;

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

        private async Task OnExecuteAsync()
        {
            _movieService.Init(File);
            //await _movieService.UpdateMovieRatingsAsync("vieekk");
            await _movieService.UpdateMoviesAsync();
            _movieService.Save(File);
            var lines = new List<string>() { "imdbID,Title,Year,WatchedDate,Rating" };
            lines.AddRange(_movieService.Movies.Values.Where(movie => movie.ImdbId != null).Select(movie => $"{movie.ImdbId},\"{movie.Title}\",{movie.Year},{movie.Ratings.FirstOrDefault()?.Date.ToString("yyyy-MM-dd")},{movie.Ratings.FirstOrDefault()?.Rating}"));
            _exportService.WriteCsv(lines, @"C:\Users\Viktor\Movies.csv");
        }
    }
}
