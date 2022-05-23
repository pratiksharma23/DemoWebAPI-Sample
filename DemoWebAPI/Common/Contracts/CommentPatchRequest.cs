using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DemoWebAPI.Common.Contracts
{
    public class CommentPatchRequest
    {
        [Required]
        [JsonProperty(PropertyName = "postId")]
        public int PostId { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
    }
}
