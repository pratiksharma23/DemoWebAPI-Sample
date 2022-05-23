using DemoWebAPI.Common.Contracts;
using DemoWebAPI.Common.Interfaces;
using DemoWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWebAPI.Implementation
{
    public class PostsManagementService : IPostsManagementService
    {
        public Task DeletePostAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Post> GetPostAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Post>> GetPostsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Post> PatchPostAsync(int id, PostPatchRequest patchRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<Post> PostPostAsync(Post comment)
        {
            throw new System.NotImplementedException();
        }

        public Task<Post> PutPostAsync(Post comment)
        {
            throw new System.NotImplementedException();
        }
    }
}
