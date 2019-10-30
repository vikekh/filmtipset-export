using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Interfaces
{
    public interface IScraperService
    {
        public Task<MovieDetailsPage> GetMovieDetailsPageAsync(string slug);

        public Task<IEnumerable<MovieRatingsPageItem>> GetMovieRatingsPageAsync(string username, int page);
    }
}
