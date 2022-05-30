using IdentityModel.Client;
using Movies.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task<Movie> CreateMovie(Movie movie)
        {
            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/movies/");
            request.Content = new StringContent(JsonConvert.SerializeObject(movie), encoding: Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();


            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var createdMovie = JsonConvert.DeserializeObject<Movie>(content);
            return createdMovie;
        }

        public async Task DeleteMovie(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/movies/" + id);

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies/" + id);

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();


            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var movie = JsonConvert.DeserializeObject<Movie>(content);
            return movie;
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

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/movies/" + movie.Id);
            request.Content = new StringContent(JsonConvert.SerializeObject(movie), encoding: Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();


            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var updatedMovie = JsonConvert.DeserializeObject<Movie>(content);
            return updatedMovie;
        }
    }
}
