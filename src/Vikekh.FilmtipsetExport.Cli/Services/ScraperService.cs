using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Extensions;
using Vikekh.FilmtipsetExport.Cli.Interfaces;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Services
{
    public class ScraperService : IScraperService
    {
        private readonly IHttpService _httpService;

        public ScraperService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<Movie>> GetMovieRatingsAsync(string username, int page)
        {
            var html = await _httpService.GetMovieRatingsAsync(username, page);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var movies = new List<Movie>();
            
            foreach (var node in htmlDocument.DocumentNode.GetListNodes())
            {
                movies.Add(new Movie
                {
                    FilmtipsetSlug = node.GetFilmtipsetSlug(),
                    FilmtipsetTitle = node.GetFilmtipsetTitle(),
                    Rating = node.GetRating(),
                    WatchedDate = node.GetWatchedDate()
                });
            }

            return movies;
        }

        public async Task<Movie> GetMovieDetailsAsync(Movie movie)
        {
            var html = await _httpService.GetMovieDetailsAsync(movie.FilmtipsetSlug);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            if (movie == null) movie = new Movie();

            movie.ImdbId = htmlDocument.DocumentNode.GetImdbId();
            movie.Year = htmlDocument.DocumentNode.GetYear();


            return movie;
        }
    }
}
