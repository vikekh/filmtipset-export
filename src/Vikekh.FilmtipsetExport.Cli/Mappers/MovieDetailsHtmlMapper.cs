using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Interfaces;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Mappers
{
    public class MovieDetailsHtmlMapper : HtmlMapperBase<MovieDetailsScrape>
    {
        public override MovieDetailsScrape Map(HtmlDocument item)
        {
            var startUrl = @"https://www.imdb.com/title/tt";

            return new MovieDetailsScrape
            {
                ImdbId = item.DocumentNode.SelectSingleNode($"//span[{XPathHasClass("postmeta")}]/i[{XPathHasClass("fa-film")}]/following-sibling::a[starts-with(@href, '{startUrl}')]")?
                    .GetAttributeValue("href", null)?
                    .Replace(startUrl, string.Empty)
                    .Insert(0, "tt"),
                //Title = item.DocumentNode.SelectNodes("//table#moviedata/tr"),
                Year = int.Parse(item.DocumentNode.SelectSingleNode($"//span[{XPathHasClass("postmeta")}]/i[{XPathHasClass("fa-calendar")}]/following-sibling::text()")?
                    .InnerText.Trim())
            };
        }
    }
}
