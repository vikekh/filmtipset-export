using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Vikekh.FilmtipsetExport.Cli.Interfaces;

namespace Vikekh.FilmtipsetExport.Cli.Services
{
    public class ExportService : IExportService
    {
        public void WriteCsv(IEnumerable<string> data, string path)
        {
            using (var file = new StreamWriter(path, false))
            {
                foreach (var item in data)
                {
                    file.WriteLine(item);
                }
            }
        }
    }
}
