using System.Collections.Generic;
using System.Threading.Tasks;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Interfaces
{
    public interface IScraperService
    {
        public Task<IEnumerable<Movie>> GetMovieGradesAsync(string username, int page);

        public Task<string> GetMovieAsync(string path);
    }
}
