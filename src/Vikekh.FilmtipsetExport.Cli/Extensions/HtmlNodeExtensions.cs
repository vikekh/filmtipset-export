using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Extensions
{
    public static class HtmlNodeExtensions
    {
        public static HtmlNodeCollection GetListNodes(this HtmlNode node)
        {
            if (node.NodeType != HtmlNodeType.Document) throw new ArgumentException();

            return node.SelectNodes("//table[@class='list']/tr");
        }

        public static string GetFilmtipsetSlug(this HtmlNode node)
        {
            if (node.Name.ToUpper() != "TR") throw new ArgumentException();

            return node.SelectSingleNode("//td[1]/a[starts-with(@href, '/film/')]")?.GetAttributeValue("href", null)?.Replace("/film/", string.Empty);
        }

        public static string GetFilmtipsetTitle(this HtmlNode node)
        {
            if (node.Name.ToUpper() != "TR") throw new ArgumentException();

            return node.SelectSingleNode("//td[1]/a")?.InnerText;
        }

        public static int GetRating(this HtmlNode node)
        {
            if (node.Name.ToUpper() != "TR") throw new ArgumentException();

            return node.SelectNodes($"//td[3]/i[{HasClass("fa-star")}]").Count;
        }

        public static DateTime GetWatchedDate(this HtmlNode node)
        {
            if (node.Name.ToUpper() != "TR") throw new ArgumentException();

            return DateTime.Parse(node.SelectSingleNode("//td[2]")?.InnerText);
        }

        public static HtmlNodeCollection GetMetaDataNodes(this HtmlNode node)
        {
            if (node.NodeType != HtmlNodeType.Document) throw new ArgumentException();

            return node.SelectNodes($"//span[{HasClass("postmeta")})]");
        }

        public static HtmlNodeCollection GetMovieDataNodes(this HtmlNode node)
        {
            if (node.NodeType != HtmlNodeType.Document) throw new ArgumentException();

            return node.SelectNodes("//table#moviedata/tr");
        }

        private static string HasClass(string className)
        {
            return $"contains(concat(' ',normalize-space(@class),' '),' {className} ')";
        }
        
        public static string GetImdbId(this HtmlNode node)
        {
            var startUrl = @"https://www.imdb.com/title/tt";

            return node.SelectSingleNode($"//span[{HasClass("postmeta")}]/i[{HasClass("fa-film")}]/following-sibling::a[starts-with(@href, '{startUrl}')]")?
                .GetAttributeValue("href", null)?
                .Replace(startUrl, string.Empty)
                .Insert(0, "tt");
        }

        public static int GetYear(this HtmlNode node)
        {
            return int.Parse(node.SelectSingleNode($"//span[{HasClass("postmeta")}]/i[{HasClass("fa-calendar")}]/following-sibling::text()")?
                .InnerText.Trim());
        }
    }
}
