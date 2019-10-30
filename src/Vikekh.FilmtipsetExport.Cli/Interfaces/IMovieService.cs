using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Interfaces
{
    public interface IMovieService
    {
        public void Init(string path);

        public IDictionary<string, Movie> Movies { get; }

        public void Save(string path);

        public Task UpdateMoviesAsync();

        public Task UpdateMovieAsync(Movie movie);

        public Task UpdateMovieRatingsAsync(string username);
    }
}
