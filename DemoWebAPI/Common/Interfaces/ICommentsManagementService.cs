using DemoWebAPI.Common.Contracts;
using DemoWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWebAPI.Common.Interfaces
{
    public interface ICommentsManagementService
    {
        Task<Comment> GetCommentAsync(int id);
        Task<List<Comment>> GetCommentsAsync();
        Task<Comment> PostCommentAsync(Comment comment);
        Task<Comment> PutCommentAsync(Comment comment);
        Task DeleteCommentAsync(int id);
        Task<Comment> PatchCommentAsync(CommentPatchRequest patchRequest);
    }
}
