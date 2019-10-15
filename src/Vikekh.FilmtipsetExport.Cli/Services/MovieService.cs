using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Vikekh.FilmtipsetExport.Cli.Models;
using Vikekh.FilmtipsetExport.Cli.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Vikekh.FilmtipsetExport.Cli.Services
{
    public class MovieService : IMovieService
    {
        private readonly IHttpService _httpService;
        private readonly IScraperService _scraperService;

        private IDictionary<string, Movie> Movies { get; set; }

        public MovieService(IHttpService httpService, IScraperService scraperService)
        {
            _httpService = httpService;
            _scraperService = scraperService;
            Movies = new Dictionary<string, Movie>();
        }

        public void Add(Movie movie)
        {
            Movies.Add(movie.FilmtipsetSlug, movie);
        }

        public void Add(IEnumerable<Movie> movies)
        {
            foreach (var movie in movies)
            {
                Add(movie);
            }
        }

        public IEnumerable<Movie> GetList()
        {
            // TODO: clone
            return Movies.Values.ToList().OrderByDescending(movie => movie.WatchedDate);
        }

        public void Init(string path)
        {
            using (var file = new StreamReader(path))
            {
                var serializer = new JsonSerializer();
                var movies = (IEnumerable<Movie>)serializer.Deserialize(file, typeof(IEnumerable<Movie>));
                Add(movies);
            }
        }

        public async Task UpdateAsync(string username)
        {
            for (var i = 0; i <= i; i++)
            {
                var movies = await _scraperService.GetMovieRatingsAsync(username, i);
                await _scraperService.GetMovieDetailsAsync(movies.First());
                Add(movies);
            }
        }

        public void Save(string path)
        {
            using (var file = new StreamWriter(path, false))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, GetList());
            }
        }
    }
}
