using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DemoWebAPI.Models
{
    public class Comment
    {
        [Required]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

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
