using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Models;
using System.Linq;

namespace Vikekh.FilmtipsetExport.Cli.Mappers
{
    public class MovieRatingsPageHtmlMapper : HtmlMapperBase<IEnumerable<MovieRatingsPageItem>>
    {
        public override IEnumerable<MovieRatingsPageItem> Map(HtmlDocument item)
        {
            var nodes = item.DocumentNode.SelectNodes("//table[@class='list']/tr");
            
            foreach (var node in nodes)
            {
                yield return new MovieRatingsPageItem
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
