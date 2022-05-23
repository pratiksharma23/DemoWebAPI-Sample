using DemoWebAPI.Common.Contracts;
using DemoWebAPI.Common.Interfaces;
using DemoWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWebAPI.Implementation
{
    public class CommentsManagementService : ICommentsManagementService
    {
        public Task DeleteCommentAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Comment> GetCommentAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Comment>> GetCommentsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Comment> PatchCommentAsync(CommentPatchRequest patchRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<Comment> PostCommentAsync(Comment comment)
        {
            throw new System.NotImplementedException();
        }

        public Task<Comment> PutCommentAsync(Comment comment)
        {
            throw new System.NotImplementedException();
        }
    }
}
