using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Vikekh.FilmtipsetExport.Cli.Interfaces;
using Vikekh.FilmtipsetExport.Cli.Mappers;
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

        public async Task<MovieDetailsPage> GetMovieDetailsPageAsync(string slug)
        {
            var response = await _httpClient.GetAsync($"/film/{slug}");
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();
            return new MovieDetailsPageHtmlMapper().Map(html);
        }

        public async Task<IEnumerable<MovieRatingsPageItem>> GetMovieRatingsPageAsync(string username, int page)
        {
            var response = await _httpClient.GetAsync($"/betyg/{username}?p={page}");
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();
            return new MovieRatingsPageHtmlMapper().Map(html);
        }
    }
}
