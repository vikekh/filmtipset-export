using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Vikekh.FilmtipsetExport.Cli.Interfaces;
using Vikekh.FilmtipsetExport.Cli.Models;

namespace Vikekh.FilmtipsetExport.Cli.Services
{
    public class ScraperService : IScraperService
    {
        private readonly HttpClient _httpClient;

        public ScraperService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("https://www.filmtipset.se/");
        }

        public async Task<IEnumerable<Movie>> GetMovieGradesAsync(string username, int page)
        {
            var response = await _httpClient.GetAsync($"/betyg/{username}?p={page}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(result);

            var table = htmlDocument.DocumentNode.SelectNodes("//table[@class='list']").FirstOrDefault();

            if (table == null) return null;

            var movies = new List<Movie>();

            foreach (var row in table.SelectNodes("tr"))
            {
                var cells = row.SelectNodes("td");
                movies.Add(new Movie
                {
                    Date = DateTime.Parse(cells[1].InnerText),
                    Grade = cells[2].SelectNodes("//i[contains(concat(' ', normalize-space(@class), ' '), ' fa-star ')]").Count,
                    Title = cells[0].FirstChild.InnerText,
                    Url = cells[0].FirstChild.Attributes["href"].Value
                });
            }

            return movies;
        }

        public async Task<string> GetMovieAsync(string path)
        {
            var response = await _httpClient.GetAsync(path);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(result);

            var links = htmlDocument.DocumentNode.SelectNodes("//a[starts-with(@href, 'https://www.imdb.com/title/')]");

            if (links.Count != 1) return null;

            return links[0].Attributes["href"].Value.Replace("https://www.imdb.com/title/", string.Empty);
        }
    }
}
