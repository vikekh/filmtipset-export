using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Interfaces
{
    public interface IScraperService
    {
        public Task<MovieDetailsScrape> GetMovieDetailsAsync(string slug);

        public Task<IEnumerable<MovieRatingScrape>> GetMovieRatingsAsync(string username, int page);
    }
}
