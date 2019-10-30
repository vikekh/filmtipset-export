using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Interfaces;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Mappers
{
    public class MovieDetailsPageHtmlMapper : HtmlMapperBase<MovieDetailsPage>
    {
        public override MovieDetailsPage Map(HtmlDocument item)
        {
            var startUrl = @"https://www.imdb.com/title/tt";

            return new MovieDetailsPage
            {
                ImdbId = item.DocumentNode.SelectSingleNode($"//span[{XPathHasClass("postmeta")}]/i[{XPathHasClass("fa-film")}]/following-sibling::a[starts-with(@href, '{startUrl}')]")?
                    .GetAttributeValue("href", null)?
                    .Replace(startUrl, string.Empty)
                    .Insert(0, "tt"),
                Title = item.DocumentNode.SelectSingleNode("//table[@id='moviedata']//td[contains(text(), 'Originaltitel')]/following-sibling::td").InnerText.Trim(),
                Year = int.Parse(item.DocumentNode.SelectSingleNode($"//span[{XPathHasClass("postmeta")}]/i[{XPathHasClass("fa-calendar")}]/following-sibling::text()")?
                    .InnerText.Trim())
            };
        }
    }
}
