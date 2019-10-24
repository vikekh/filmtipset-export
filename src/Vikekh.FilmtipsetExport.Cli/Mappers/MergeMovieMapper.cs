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
            if (type.IsValueType) return Activator.CreateInstance(type);

            return null;
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
            var movie = new Movie();
            var properties = typeof(Movie).GetProperties();

            foreach (var property in properties)
            {
                var value1 = property.GetValue(movie1);
                var value2 = property.GetValue(movie2);

                if (value1 != GetDefaultValue(property.PropertyType))
                {
                    property.SetValue(movie, value1);
                }

                if (value2 != GetDefaultValue(property.PropertyType))
                {
                    property.SetValue(movie, value2);
                }
            }

            return movie;
        }
    }
}
