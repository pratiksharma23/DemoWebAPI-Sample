using DemoWebAPI.Common.Contracts;
using DemoWebAPI.Common.Interfaces;
using DemoWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<CommentsController> _log;
        private readonly ICommentsManagementService _commentsManagementService;

        /// <summary>
		/// Comments controller constructor 
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="commentsManagementService"></param>
        public CommentsController(ILogger<CommentsController> logger, ICommentsManagementService commentsManagementService)
        {
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
            _commentsManagementService = commentsManagementService ?? throw new ArgumentNullException(nameof(commentsManagementService));
        }

        #region Actions
        /// <summary>
        /// Get Comment for the input commentId
        /// </summary>
        /// <param name="commentId"></param>
        [HttpGet("{commentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Comment))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = "Comment_Get")]
        public async Task<ActionResult<Comment>> GetCommentAsync(string commentId)
        {
            _log.LogInformation("Comment_Get");
            if (string.IsNullOrEmpty(commentId))
            {
                var error = $"Bad request: CommentId required";
                _log.LogError(error);
                return BadRequest(error);
            }
            var resource = await _commentsManagementService.GetCommentAsync(int.Parse(commentId));
            if (resource == null)
            {
                var error = $"Unable to locate resource with commentId: {commentId}.";
                _log.LogError(error);
                return NotFound(error);
            }
            else
            {
                _log.LogInformation($"Successfully retrieved resource with id: {commentId}");
                return Ok(resource);
            }
        }

        /// <summary>
        /// Get Comments
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Comment>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = "Comments_Get")]
        public async Task<ActionResult<List<Comment>>> GetCommentsAsync()
        {
            _log.LogInformation("Comments_Get");
            var resource = await _commentsManagementService.GetCommentsAsync();
            if (resource == null || resource.Count == 0)
            {
                var error = "Unable to find comments";
                _log.LogError(error);
                return NotFound(error);
            }
            else
            {
                _log.LogInformation("Successfully retrieved comments");
                return Ok(resource);
            }
        }

        /// <summary>
        /// Create Comment for the input request
        /// </summary>
        /// <param name="commentRequest"></param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Comment))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = "Comment_Post")]
        public async Task<ActionResult<Comment>> CreateCommentAsync([FromBody] Comment commentRequest)
        {
            _log.LogInformation("Comment_Post");
            if (commentRequest == null)
            {
                var error = "Bad request: body can not be null";
                _log.LogInformation(error);
                return BadRequest(error);
            }
            var resource = await _commentsManagementService.PostCommentAsync(commentRequest);
            if (resource == null)
            {
                var error = "Unable to create resource.";
                _log.LogInformation(error);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            else
            {
                _log.LogInformation($"Successfully created resource: {resource}");
                return StatusCode(StatusCodes.Status201Created, resource);
            }
        }

        /// <summary>
        /// Update Comment for the input request
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="commentPatchRequest"></param>
        [HttpPatch("{commentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Comment))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = "Comment_Patch")]
        public async Task<ActionResult<Comment>> UpdateCommentAsync(int commentId, [FromBody] CommentPatchRequest commentPatchRequest)
        {
            _log.LogInformation("Comment_Patch");
            if (commentPatchRequest == null)
            {
                var error = "Invalid request: body can not be null";
                _log.LogError(error);
                return BadRequest(error);
            }
            var resource = await _commentsManagementService.PatchCommentAsync(commentId, commentPatchRequest);
            if (resource == null)
            {
                var error = $"Unable to patch Comment for Id: {commentId}";
                _log.LogError(error);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            else
            {
                _log.LogInformation("Successfully patched comment for id: {commentId}");
                return Ok(resource);
            }
        }

        /// <summary>
        /// Put Comment for the input request
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="commentRequest"></param>
        [HttpPut("{commentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Comment))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = "Comment_Put")]
        public async Task<ActionResult<Comment>> PutCommentAsync(int commentId, [FromBody] Comment commentRequest)
        {
            _log.LogInformation("Comment_Put");
            if (commentRequest == null)
            {
                var error = "Invalid request: body can not be null";
                _log.LogError(error);
                return BadRequest(error);
            }
            var resource = await _commentsManagementService.PutCommentAsync(commentId, commentRequest);
            if (resource == null)
            {
                var error = $"Unable to update Comment for Id: {commentId}";
                _log.LogError(error);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            else
            {
                _log.LogInformation($"Successfully updated comment for id: {commentId}");
                return Ok(resource);
            }
        }

        /// <summary>
        /// Delete the post for a given ID
        /// </summary>
        [HttpDelete("{commentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = "Comment_Delete")]
        public async Task<ActionResult> DeleteCommentAsync(int commentId)
        {
            _log.LogInformation("Comment_Delete");
            try
            {
                await _commentsManagementService.DeleteCommentAsync(commentId);
                return NoContent();
            }
            catch (Exception ex)
            {
                var error = $"Unable to delete post for id: {commentId}";
                _log.LogError(error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
        #endregion
    }
}
