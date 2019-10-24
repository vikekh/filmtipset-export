using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Mappers
{
    public class MovieRatingScrapeToMovieMapper : MapperBase<MovieRatingScrape, Movie>
    {
        public override MovieRatingScrape Map(Movie item)
        {
            throw new NotSupportedException();
        }

        public override Movie Map(MovieRatingScrape item)
        {
            var ratings = new List<MovieRating>();
            ratings.Add(new MovieRating
            {
                Date = item.Date,
                Rating = item.Rating
            });

            return new Movie
            {
                Ratings = ratings,
                Slug = item.Slug,
                SwedishTitle = item.SwedishTitle,
            };
        }
    }
}
