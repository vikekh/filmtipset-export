﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vikekh.FilmtipsetExport.Cli.Models
{
    public class Movie
    {
        public string ImdbId { get; set; }

        public IEnumerable<MovieRating> Ratings { get; set; }

        public string Slug { get; set; }

        public string SwedishTitle { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }
    }
}
