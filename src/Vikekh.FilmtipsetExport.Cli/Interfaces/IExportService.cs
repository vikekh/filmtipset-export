using System;
using System.Collections.Generic;
using System.Text;

namespace Vikekh.FilmtipsetExport.Cli.Interfaces
{
    public interface IExportService
    {
        public void WriteCsv(IEnumerable<string> data, string path);
    }
}
