using System;
using System.Collections.Generic;
using System.Text;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Interfaces
{
    public interface IMovieService
    {
        public IEnumerable<Movie> GetList();

        public void Init(string path);
    }
}
