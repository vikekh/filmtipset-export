using System.Collections.Generic;
using System.Threading.Tasks;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Interfaces
{
    public interface IScraperService
    {
        public Task<IEnumerable<Movie>> GetMovieRatingsAsync(string username, int page);

        public Task<Movie> GetMovieDetailsAsync(Movie movie);
    }
}
