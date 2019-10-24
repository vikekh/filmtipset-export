using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Vikekh.FilmtipsetExport.Cli.Models;
using Vikekh.FilmtipsetExport.Cli.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Vikekh.FilmtipsetExport.Cli.Mappers;

namespace Vikekh.FilmtipsetExport.Cli.Services
{
    public class MovieService : IMovieService
    {
        private readonly IScraperService _scraperService;

        public IDictionary<string, Movie> Movies { get; private set; }

        public MovieService(IScraperService scraperService)
        {
            _scraperService = scraperService;
            Movies = new List<Movie>();
        }

        public IEnumerable<Movie> GetList()
        {
            // TODO: clone
            return Movies.ToList().OrderByDescending(movie => movie.Ratings?.OrderByDescending(movieRating => movieRating.Date).FirstOrDefault());
        }

        public void Init(string path)
        {
            //using (var file = new StreamReader(path))
            //{
            //    var serializer = new JsonSerializer();
            //    var movies = (IEnumerable<Movie>)serializer.Deserialize(file, typeof(IEnumerable<Movie>));
            //    Movies.ToList().
            //}
        }

        public async Task<Movie> Update(Movie movie)
        {
            var movieDetails = await _scraperService.GetMovieDetailsAsync(movie.Slug);
            return new MergeMovieMapper().Map(movie, new MovieDetailsScrapeToMovieMapper().Map(movieDetails));
        }

        public async Task<Movie> GetMovie()

        public async Task UpdateDetailsAsync()
        {
            foreach (var movie in Movies.Keys))
            {
                var index = Movies.IndexOf(movie);
                Movies[ = await Update(movie);
            }
        }

        public async Task UpdateAsync(string username)
        {
            for (var i = 0; i <= 0; i++)
            {
                var movieRatings = await _scraperService.GetMovieRatingsAsync(username, i);
                var j = 0;

                foreach (var movieRating in movieRatings)
                {
                    if (j == 5) break;

                    var movie = new MovieRatingScrapeToMovieMapper().Map(movieRating);
                    Movies.Add(movie.Slug, movie);
                    j++;
                }
            }

            await UpdateDetailsAsync();
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
