using System.ComponentModel.DataAnnotations;

namespace Agency.MVC.ViewModels.Account
{
	public class LoginVM
	{
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
