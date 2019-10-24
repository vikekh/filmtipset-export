using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Mappers
{
    public class MovieDetailsScrapeToMovieMapper : MapperBase<MovieDetailsScrape, Movie>
    {
        public override MovieDetailsScrape Map(Movie item)
        {
            throw new NotSupportedException();
        }

        public override Movie Map(MovieDetailsScrape item)
        {
            return new Movie
            {
                ImdbId = item.ImdbId,
                Title = item.Title,
                Year = item.Year
            };
        }
    }
}
