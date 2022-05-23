using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DemoWebAPI.Models
{
    public class Post
    {
        [Required]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
    }
}
