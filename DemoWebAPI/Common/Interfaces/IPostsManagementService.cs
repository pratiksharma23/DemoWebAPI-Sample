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
        Task<Post> PostPostAsync(Post comment);
        Task<Post> PutPostAsync(Post comment);
        Task DeletePostAsync(int id);
        Task<Post> PatchPostAsync(int id, PostPatchRequest patchRequest);
    }
}
