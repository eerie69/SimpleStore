using System.ComponentModel.DataAnnotations;

namespace KazahStore.ViewModels
{
    public class HomeUserCreateViewModel
    {
        [Display(Name = "Имя участника:")]
        [Required]
        public string? UserName { get; set; }
        [Display(Name = "Эл. почта:")]
        [Required(ErrorMessage = "Email address is required")]

        public string? EmailAddress { get; set; }
        [Display(Name = "Пароль:")]
        [Required]
        [DataType(DataType.Password)]

        public string? Password { get; set; }
        [Display(Name = "Подтвердите пароль:")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]

        public string? ConfirmPassword { get; set; }
    }
}
