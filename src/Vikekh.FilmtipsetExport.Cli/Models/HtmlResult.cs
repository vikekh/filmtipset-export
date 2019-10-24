using System;
using System.Collections.Generic;
using System.Text;
using Vikekh.FilmtipsetExport.Cli.Interfaces;

namespace Vikekh.FilmtipsetExport.Cli.Models
{
    public class HtmlResult : IHtmlResult
    {
        public string Html { get; private set; }

        public Uri Uri { get; private set; }

        public HtmlResult(Uri uri, string html)
        {
            Uri = uri;
            Html = html;
        }
    }
}
