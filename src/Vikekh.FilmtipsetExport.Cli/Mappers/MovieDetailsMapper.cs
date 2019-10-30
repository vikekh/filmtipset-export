using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Mappers
{
    public class MovieDetailsMapper : MapperBase<MovieDetailsPage, Movie>
    {
        public override MovieDetailsPage Map(Movie item)
        {
            throw new NotSupportedException();
        }

        public override Movie Map(MovieDetailsPage item)
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
