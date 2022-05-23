using DemoWebAPI.Common.Contracts;
using DemoWebAPI.Common.Interfaces;
using DemoWebAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DemoWebAPI.Implementation
{
    public class PostsManagementService : IPostsManagementService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PostsManagementService> _logger;
        public PostsManagementService(ILogger<PostsManagementService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            try
            {
                _httpClient = GetHttpClient();
            }
            catch(Exception e)
            {
                _logger.LogError($"Error while initializing PostsManagementService: {e.Message}");
                throw;
            }
        }
        public async Task DeletePostAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"posts/{id}");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Successfully deleted post with Id: {id}");
            }
            else
            {
                var error = $"Unable to delete post with id: {id}";
                _logger.LogError(error, response.StatusCode);
                throw new Exception(error);
            }
        }

        public async Task<Post> GetPostAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"posts/{id}");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully received post");
                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var post = JsonConvert.DeserializeObject<Post>(responseContent);
                    if (post == null)
                    {
                        var error = "Received empty content while fetching post";
                        _logger.LogError(error);
                        throw new Exception(error);
                    }
                    return post;
                }
                else
                {
                    var error = "Received empty content while fetching post";
                    _logger.LogError(error);
                    throw new Exception(error);
                }
            }
            else
            {
                var error = "Unable to fetch post";
                _logger.LogError(error, response.StatusCode);
                throw new Exception(error);
            }
        }

        public async  Task<List<Post>> GetPostsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("posts");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully received posts");
                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var posts = JsonConvert.DeserializeObject<List<Post>>(responseContent);
                    if (posts == null)
                    {
                        var error = "Received empty content while fetching posts";
                        _logger.LogError(error);
                        throw new Exception(error);
                    }
                    return posts;
                }
                else
                {
                    var error = "Received empty content while fetching posts";
                    _logger.LogError(error);
                    throw new Exception(error);
                }
            }
            else
            {
                var error = "Unable to fetch posts";
                _logger.LogError(error, response.StatusCode);
                throw new Exception(error);
            }
        }

        public async Task<Post> PatchPostAsync(int id, PostPatchRequest patchRequest)
        {
            string json = JsonConvert.SerializeObject(patchRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PatchAsync($"posts/{id}", httpContent);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Successfully patched post");
                if (result.Content != null)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    var post = JsonConvert.DeserializeObject<Post>(responseContent);
                    if (post == null)
                    {
                        var error = "Received empty content in patch post response";
                        _logger.LogError(error);
                        throw new Exception(error);
                    }
                    return post;
                }
                else
                {
                    var error = "Received empty content in patch post response";
                    _logger.LogError(error);
                    throw new Exception(error);
                }
            }
            else
            {
                var error = "Unable to patch post";
                _logger.LogError(error, result.StatusCode);
                throw new Exception(error);
            }
        }

        public async Task<Post> PostPostAsync(Post postRequest)
        {
            string json = JsonConvert.SerializeObject(postRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("posts", httpContent);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Successfully created post");
                if (result.Content != null)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    var post = JsonConvert.DeserializeObject<Post>(responseContent);
                    if (post == null)
                    {
                        var error = "Received empty content in create post response";
                        _logger.LogError(error);
                        throw new Exception(error);
                    }
                    return post;
                }
                else
                {
                    var error = "Received empty content in create post response";
                    _logger.LogError(error);
                    throw new Exception(error);
                }
            }
            else
            {
                var error = "Unable to create post";
                _logger.LogError(error, result.StatusCode);
                throw new Exception(error);
            }
        }

        public async Task<Post> PutPostAsync(int postId, Post postRequest)
        {
            string json = JsonConvert.SerializeObject(postRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync($"posts/{postId}", httpContent);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Successfully updated post");
                if (result.Content != null)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    var post = JsonConvert.DeserializeObject<Post>(responseContent);
                    if (post == null)
                    {
                        var error = "Received empty content in update post response";
                        _logger.LogError(error);
                        throw new Exception(error);
                    }
                    return post;
                }
                else
                {
                    var error = "Received empty content in update post response";
                    _logger.LogError(error);
                    throw new Exception(error);
                }
            }
            else
            {
                var error = "Unable to update post";
                _logger.LogError(error, result.StatusCode);
                throw new Exception(error);
            }
        }

        private static HttpClient GetHttpClient()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("DemoWebAPI");
            return client;
        }
    }
}
