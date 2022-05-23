using DemoWebAPI.Common.Contracts;
using DemoWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWebAPI.Common.Interfaces
{
    public interface IPostsManagementService
    {
        Task<Post> GetPostAsync(int id);
        Task<List<Post>> GetPostsAsync();
        Task<Post> PostPostAsync(Post post);
        Task<Post> PutPostAsync(int postId, Post post);
        Task DeletePostAsync(int id);
        Task<Post> PatchPostAsync(int id, PostPatchRequest patchRequest);
    }
}
