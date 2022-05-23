using Newtonsoft.Json;

namespace DemoWebAPI.Common.Contracts
{
    public class PostPatchRequest
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public int UserId { get; set; }
    }
}
