using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage="Email is Required")]
        [EmailAddress(ErrorMessage ="Format Email is invalid")]
        [StringLength(100,ErrorMessage ="Email length invalid, Max {1}")]
        public string Email { get; set; }
    }
}
