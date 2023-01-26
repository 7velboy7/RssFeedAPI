using System.ComponentModel.DataAnnotations;

namespace RssFeed.DTOs.Requests
{
    public class CreateFeedRequestModel 
    {
        [Required]
        public Uri Link { get; set; }
    }
}
