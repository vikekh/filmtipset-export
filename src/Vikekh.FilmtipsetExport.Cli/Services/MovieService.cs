using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Vikekh.FilmtipsetExport.Cli.Models;
using Vikekh.FilmtipsetExport.Cli.Interfaces;
using System.Linq;

namespace Vikekh.FilmtipsetExport.Cli.Services
{
    public class MovieService : IMovieService
    {
        private readonly IScraperService _scraperService;

        private IDictionary<string, Movie> Movies { get; set; }

        public MovieService(IScraperService scraperService)
        {
            _scraperService = scraperService;
        }

        public IEnumerable<Movie> GetList()
        {
            // TODO: clone
            return Movies.Values.ToList().OrderByDescending(movie => movie.Date);
        }

        public void Init(string path)
        {
            using (var file = new StreamReader(path))
            {
                var serializer = new JsonSerializer();
                Movies = (Dictionary<string, Movie>)serializer.Deserialize(file, typeof(IDictionary<string, Movie>));
            }
        }
    }
}
