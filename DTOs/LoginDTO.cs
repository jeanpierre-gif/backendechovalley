using System.ComponentModel.DataAnnotations;

namespace training.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string email {  get; set; }
        public string password { get; set; }
    }
}
