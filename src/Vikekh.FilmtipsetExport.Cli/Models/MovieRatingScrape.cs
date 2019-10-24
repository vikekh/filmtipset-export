using System;
using System.Collections.Generic;
using System.Text;

namespace Vikekh.FilmtipsetExport.Cli.Models
{
    public class MovieRatingScrape
    {
        public int Rating { get; set; }
        
        public string Slug { get; set; }

        public string SwedishTitle { get; set; }

        public DateTime Date { get; set; }
    }
}
