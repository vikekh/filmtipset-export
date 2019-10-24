using System;
using System.Collections.Generic;
using System.Text;
using Vikekh.FilmtipsetExport.Cli.Interfaces;

namespace Vikekh.FilmtipsetExport.Cli.Mappers
{
    public abstract class MapperBase<T1, T2> : IMapper<T1, T2>
    {
        public abstract T1 Map(T2 item);

        public abstract T2 Map(T1 item);
    }
}
