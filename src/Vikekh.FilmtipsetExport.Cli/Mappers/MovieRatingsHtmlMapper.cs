using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Models;
using System.Linq;

namespace Vikekh.FilmtipsetExport.Cli.Mappers
{
    public class MovieRatingsHtmlMapper : HtmlMapperBase<IEnumerable<MovieRatingScrape>>
    {
        public override IEnumerable<MovieRatingScrape> Map(HtmlDocument item)
        {
            var nodes = item.DocumentNode.SelectNodes("//table[@class='list']/tr");
            
            foreach (var node in nodes)
            {
                yield return new MovieRatingScrape
                {
                    Date = DateTime.Parse(node.SelectSingleNode(".//td[2]")?.InnerText),
                    Slug = node.SelectSingleNode(".//td[1]/a[starts-with(@href, '/film/')]")?
                        .GetAttributeValue("href", null)?
                        .Replace("/film/", string.Empty),
                    SwedishTitle = node.SelectSingleNode(".//td[1]/a")?.InnerText,
                    Rating = node.SelectNodes($".//td[3]/i[{XPathHasClass("fa-star")}]").Count
                };
            }
        }
    }
}
