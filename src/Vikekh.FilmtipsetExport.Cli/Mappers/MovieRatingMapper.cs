using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Mappers
{
    public class MovieRatingMapper : MapperBase<MovieRatingsPageItem, Movie>
    {
        public override MovieRatingsPageItem Map(Movie item)
        {
            throw new NotSupportedException();
        }

        public override Movie Map(MovieRatingsPageItem item)
        {
            return new Movie
            {
                Ratings = new List<MovieRating>()
                {
                    new MovieRating
                    {
                        Date = item.Date,
                        Rating = item.Rating
                    }
                },
                Slug = item.Slug,
                SwedishTitle = item.SwedishTitle,
            };
        }
    }
}
