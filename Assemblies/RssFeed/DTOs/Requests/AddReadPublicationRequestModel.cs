using System.ComponentModel.DataAnnotations;

namespace RssFeed.DTOs.Requests
{
    public class AddReadPublicationRequestModel
    {
        [Required]
        public Uri PublicationLink { get; set; } 

    }
}
