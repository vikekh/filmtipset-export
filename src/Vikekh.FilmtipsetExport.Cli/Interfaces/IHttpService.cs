using System.Collections.Generic;
using System.Threading.Tasks;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Interfaces
{
    public interface IHttpService
    {
        public Task<string> GetMovieRatingsAsync(string username, int page);

        public Task<string> GetMovieDetailsAsync(string slug);
    }
}
