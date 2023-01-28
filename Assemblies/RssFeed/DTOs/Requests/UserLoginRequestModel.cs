using System.ComponentModel.DataAnnotations;

namespace RssFeed.DTOs.Requests
{
    public class UserLoginRequestModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Is required")]
        public string Password { get; set; }
    }
}
