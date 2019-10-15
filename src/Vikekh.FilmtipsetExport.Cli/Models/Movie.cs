using System;
using System.Collections.Generic;
using System.Text;

namespace Vikekh.FilmtipsetExport.Cli.Models
{
    public class Movie
    {
        public string FilmtipsetSlug { get; set; }

        public string FilmtipsetTitle { get; set; }

        public string ImdbId { get; set; }

        public int Rating { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime WatchedDate { get; set; }

        public int Year { get; set; }
    }
}
