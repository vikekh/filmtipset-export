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
            Movies = new Dictionary<string, Movie>();
        }

        public void Init(string path)
        {
            using (var file = new StreamReader(path))
            {
                var serializer = new JsonSerializer();
                var movies = (IEnumerable<Movie>)serializer.Deserialize(file, typeof(IEnumerable<Movie>));
                
                foreach (var movie in movies)
                {
                    if (movie.Slug == null)
                    {
                        throw new Exception();
                    }

                    Movies.Add(movie.Slug, movie);
                }
            }
        }

        public void Save(string path)
        {
            using (var file = new StreamWriter(path, false))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, Movies.Values.OrderByDescending(movie => movie.Ratings.FirstOrDefault()?.Date));
            }
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            var movieDetailsPage = await _scraperService.GetMovieDetailsPageAsync(movie.Slug);
            var updatedMovie = new MovieDetailsMapper().Map(movieDetailsPage);
            new MergeMovieMapper().Map(movie, updatedMovie);
        }

        public async Task UpdateMoviesAsync()
        {
            var i = 0;

            foreach (var item in Movies.Where(item => item.Value?.ImdbId == null))
            {
                if (i == 1000) break;

                try
                {
                    Console.WriteLine($"Updating movie {item.Key}");
                    await UpdateMovieAsync(item.Value);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Updating movie {item.Key} failed");
                }
                
                Console.WriteLine($"{i}");
                i++;
            }
        }

        public async Task UpdateMovieRatingsAsync(string username)
        {
            for (var i = 0; i <= 37; i++)
            {
                var movieRatingsPage = await _scraperService.GetMovieRatingsPageAsync(username, i);
                var j = 0;

                foreach (var movieRatingsPageItem in movieRatingsPage)
                {
                    //if (j == 5) break;

                    var movie = new MovieRatingMapper().Map(movieRatingsPageItem);
                    Movies.Add(movie.Slug, movie);
                    j++;
                }
            }
        }
    }
}
