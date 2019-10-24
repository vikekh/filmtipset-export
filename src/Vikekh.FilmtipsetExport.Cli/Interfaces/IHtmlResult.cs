using System;
using System.Collections.Generic;
using System.Text;

namespace Vikekh.FilmtipsetExport.Cli.Interfaces
{
    public interface IHtmlResult
    {
        public string Html { get; }

        public Uri Uri { get; }
    }
}
