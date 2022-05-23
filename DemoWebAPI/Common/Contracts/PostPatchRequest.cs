using Newtonsoft.Json;

namespace DemoWebAPI.Common.Contracts
{
    public class PostPatchRequest
    {
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "userId", NullValueHandling = NullValueHandling.Ignore)]
        public int UserId { get; set; }
    }
}
