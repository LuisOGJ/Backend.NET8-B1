﻿using Backend2.DTOs;
using System.Text.Json;

namespace Backend2.Services
{
    public class PostsService : IPostsService
    {

        // https://jsonplaceholder.typicode.com/posts

        private HttpClient _httpClient;

        public PostsService() { 
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<PostDto>> Get()
        {
            string url = "https://jsonplaceholder.typicode.com/posts";
            var result = await _httpClient.GetAsync(url);
            var body = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { 
                PropertyNameCaseInsensitive = true,
            };

            var post = JsonSerializer.Deserialize<IEnumerable<PostDto>>(body, options);
            return post;
        }
    }
}
