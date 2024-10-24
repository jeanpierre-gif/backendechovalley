using Microsoft.AspNetCore.Identity;

namespace training.Models
{
    public class UserModel : IdentityUser
    {
        public string firstname { get; set; }
        public string lastname { get; set; }

    }
}
