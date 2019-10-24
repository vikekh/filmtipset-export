using System;
using System.Collections.Generic;
using System.Text;

namespace Vikekh.FilmtipsetExport.Cli.Interfaces
{
    public interface IMapper<T1, T2>
    {
        public T1 Map(T2 item);

        public T2 Map(T1 item);
    }
}
