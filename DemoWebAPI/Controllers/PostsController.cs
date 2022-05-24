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
    /// <summary>
	/// Posts controller
	/// </summary>
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _log;
        private readonly IPostsManagementService _postsManagementService;

        /// <summary>
		/// Posts controller constructor 
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="postsManagementService"></param>
        public PostsController(ILogger<PostsController> logger, IPostsManagementService postsManagementService)
        {
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
            _postsManagementService = postsManagementService ?? throw new ArgumentNullException(nameof(postsManagementService));
        }

        #region Actions
        /// <summary>
        /// Get Post for the input postId
        /// </summary>
        /// <param name="postId"></param>
        [HttpGet("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Post))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = "Post_Get")]
        public async Task<ActionResult<Post>> GetPostAsync(string postId)
        {
            _log.LogInformation("Post_Get");
            if (string.IsNullOrEmpty(postId))
            {
                var error = $"Bad request: PostId required";
                _log.LogError(error);
                return BadRequest(error);
            }
            var resource = await _postsManagementService.GetPostAsync(int.Parse(postId));
            if (resource == null)
            {
                var error = $"Unable to locate resource with postId: {postId}.";
                _log.LogError(error);
                return NotFound(error);
            }
            else
            {
                _log.LogInformation($"Successfully retrieved resource with id: {postId}");
                return Ok(resource);
            }
        }

        /// <summary>
        /// Get Posts
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Post>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = "Posts_Get")]
        public async Task<ActionResult<List<Post>>> GetPostsAsync()
        {
            _log.LogInformation("Posts_Get");
            var resource = await _postsManagementService.GetPostsAsync();
            if (resource == null || resource.Count == 0 )
            {
                var error = "Unable to find posts";
                _log.LogError(error);
                return NotFound(error);
            }
            else
            {
                _log.LogInformation("Successfully retrieved posts");
                return Ok(resource);
            }
        }

        /// <summary>
        /// Create Post for the input request
        /// </summary>
        /// <param name="postRequest"></param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Post))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = "Post_Post")]
        public async Task<ActionResult<Post>> CreatePostAsync([FromBody] Post postRequest)
        {
            _log.LogInformation("Post_Post");
            if (postRequest == null)
            {
                var error = "Bad request: body can not be null";
                _log.LogInformation(error);
                return BadRequest(error);
            }
            var resource = await _postsManagementService.PostPostAsync(postRequest);
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
        /// Put Post for the input request
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="postRequest"></param>
        [HttpPut("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Post))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = "Post_Put")]
        public async Task<ActionResult<Post>> PutPostAsync(int postId, [FromBody] Post postRequest)
        {
            _log.LogInformation("Post_Put");
            if (postRequest == null)
            {
                var error = "Invalid request: body can not be null";
                _log.LogError(error);
                return BadRequest(error);
            }
            var resource = await _postsManagementService.PutPostAsync(postId, postRequest);
            if (resource == null)
            {
                var error = $"Unable to update Post";
                _log.LogError(error);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            else
            {
                _log.LogInformation("Successfully updated post");
                return Ok(resource);
            }
        }

        /// <summary>
        /// Delete the post for a given ID
        /// </summary>
        [HttpDelete("{postId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(OperationId = "Post_Delete")]
        public async Task<ActionResult> DeletePostAsync(int postId)
        {
            _log.LogInformation("Post_Delete");
            try
            {
                await _postsManagementService.DeletePostAsync(postId);
                return NoContent();
            }
            catch (Exception ex)
            {
                var error = $"Unable to delete post for id: {postId}";
                _log.LogError(error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
        #endregion
    }
}
