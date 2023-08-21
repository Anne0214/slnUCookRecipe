using System.ComponentModel.DataAnnotations;

namespace prjMSITUCook.Models.Parameter
{
    public class LoginParameter
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
