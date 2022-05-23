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
    public class CommentsManagementService : ICommentsManagementService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CommentsManagementService> _logger;
        public CommentsManagementService(ILogger<CommentsManagementService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            try
            {
                _httpClient = GetHttpClient();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while initializing CommentsManagementService: {e.Message}");
                throw;
            }
        }
        public async Task DeleteCommentAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"comments/{id}");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Successfully deleted comment with Id: {id}");
            }
            else
            {
                var error = $"Unable to delete comment with id: {id}";
                _logger.LogError(error, response.StatusCode);
                throw new Exception(error);
            }
        }

        public async Task<Comment> GetCommentAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"comments/{id}");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully received comment");
                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var comment = JsonConvert.DeserializeObject<Comment>(responseContent);
                    if (comment == null)
                    {
                        var error = "Received empty content while fetching Comment";
                        _logger.LogError(error);
                        throw new Exception(error);
                    }
                    return comment;
                }
                else
                {
                    var error = "Received empty content while fetching Comment";
                    _logger.LogError(error);
                    throw new Exception(error);
                }
            }
            else
            {
                var error = "Unable to fetch Comment";
                _logger.LogError(error, response.StatusCode);
                throw new Exception(error);
            }
        }

        public async Task<List<Comment>> GetCommentsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("comments");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully received Comments");
                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var comments = JsonConvert.DeserializeObject<List<Comment>>(responseContent);
                    if (comments == null)
                    {
                        var error = "Received empty content while fetching Comments";
                        _logger.LogError(error);
                        throw new Exception(error);
                    }
                    return comments;
                }
                else
                {
                    var error = "Received empty content while fetching Comments";
                    _logger.LogError(error);
                    throw new Exception(error);
                }
            }
            else
            {
                var error = "Unable to fetch comments";
                _logger.LogError(error, response.StatusCode);
                throw new Exception(error);
            }
        }

        public async Task<Comment> PatchCommentAsync(int id, CommentPatchRequest patchRequest)
        {
            string json = JsonConvert.SerializeObject(patchRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PatchAsync($"comments/{id}", httpContent);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Successfully updated Comment");
                if (result.Content != null)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    var comment = JsonConvert.DeserializeObject<Comment>(responseContent);
                    if (comment == null)
                    {
                        var error = "Received empty content in update Comment response";
                        _logger.LogError(error);
                        throw new Exception(error);
                    }
                    return comment;
                }
                else
                {
                    var error = "Received empty content in update Comment response";
                    _logger.LogError(error);
                    throw new Exception(error);
                }
            }
            else
            {
                var error = "Unable to update Comment";
                _logger.LogError(error, result.StatusCode);
                throw new Exception(error);
            }
        }

        public async Task<Comment> PostCommentAsync(Comment commentRequest)
        {
            string json = JsonConvert.SerializeObject(commentRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("comments", httpContent);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Successfully created comment");
                if (result.Content != null)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    var comment = JsonConvert.DeserializeObject<Comment>(responseContent);
                    if (comment == null)
                    {
                        var error = "Received empty content in create comment response";
                        _logger.LogError(error);
                        throw new Exception(error);
                    }
                    return comment;
                }
                else
                {
                    var error = "Received empty content in create comment response";
                    _logger.LogError(error);
                    throw new Exception(error);
                }
            }
            else
            {
                var error = "Unable to create comment";
                _logger.LogError(error, result.StatusCode);
                throw new Exception(error);
            }
        }

        public async Task<Comment> PutCommentAsync(int commentId, Comment commentRequest)
        {
            string json = JsonConvert.SerializeObject(commentRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync($"comments/{commentId}", httpContent);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Successfully created comment");
                if (result.Content != null)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    var comment = JsonConvert.DeserializeObject<Comment>(responseContent);
                    if (comment == null)
                    {
                        var error = "Received empty content in put comment response";
                        _logger.LogError(error);
                        throw new Exception(error);
                    }
                    return comment;
                }
                else
                {
                    var error = "Received empty content in put comment response";
                    _logger.LogError(error);
                    throw new Exception(error);
                }
            }
            else
            {
                var error = "Unable to put comment";
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
