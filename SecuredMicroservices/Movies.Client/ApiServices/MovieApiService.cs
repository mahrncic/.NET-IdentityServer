using IdentityModel.Client;
using Movies.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MovieApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteMovie(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Movie> GetMovieById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies/");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var movieList = JsonConvert.DeserializeObject<IEnumerable<Movie>>(content);
            return movieList;
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }
    }
}
