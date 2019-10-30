using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Mappers
{
    public class MergeMovieMapper : MapperBase<(Movie, Movie), Movie>
    {
        private static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public override (Movie, Movie) Map(Movie item)
        {
            throw new NotSupportedException();
        }

        public override Movie Map((Movie, Movie) item)
        {
            return Map(item.Item1, item.Item2);
        }

        public Movie Map(Movie movie1, Movie movie2)
        {
            var properties = typeof(Movie).GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(movie2);

                if (value != GetDefaultValue(property.PropertyType))
                {
                    property.SetValue(movie1, value);
                }
            }

            return movie1;
        }
    }
}
