using Newtonsoft.Json;

namespace DemoWebAPI.Common.Contracts
{
    public class CommentPatchRequest
    {
        [JsonProperty(PropertyName = "postId")]
        public int PostId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
