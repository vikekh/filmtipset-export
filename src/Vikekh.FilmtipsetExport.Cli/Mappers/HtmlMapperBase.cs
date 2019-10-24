using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Interfaces;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Mappers
{
    public abstract class HtmlMapperBase<T> : MapperBase<T, HtmlDocument>
    {
        public T Map(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return Map(htmlDocument);
        }

        public override HtmlDocument Map(T item) => throw new NotSupportedException();

        public static string XPathHasClass(string className)
        {
            return $"contains(concat(' ',normalize-space(@class),' '),' {className} ')";
        }
    }
}
