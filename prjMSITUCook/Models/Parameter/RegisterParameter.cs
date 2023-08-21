using System.ComponentModel.DataAnnotations;

namespace prjMSITUCook.Models.Parameter
{
    public class RegisterParameter
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string NickName { get; set; }

    }
}
