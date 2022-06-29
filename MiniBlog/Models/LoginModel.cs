using System.ComponentModel.DataAnnotations;

namespace MiniBlog.Models
{
    public class LoginModel
    {
        [Required, DataType(DataType.Password), StringLength(16, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
        [Required, StringLength(50)]
        public string Nickname { get; set; } = string.Empty;
        public bool Remember { get; set; } = false;
    }
}
