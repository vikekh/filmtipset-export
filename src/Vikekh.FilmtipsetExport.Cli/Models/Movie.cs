using System;
using System.Collections.Generic;
using System.Text;

namespace Vikekh.FilmtipsetExport.Cli.Models
{
    public class Movie
    {
        public DateTime Date { get; set; }

        public int Grade { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string ImdbId { get; set; }
    }
}
