using System.ComponentModel.DataAnnotations;

namespace DCAPP.Models;

public class RegisterViewModel
{

    [Required(ErrorMessage = "Ad zorunludur.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Soyad zorunludur.")]
    public required string LastName { get; set; }
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    public required string Username { get; set; }
    [Required(ErrorMessage = "E-posta adresi zorunludur.")]
    [EmailAddress]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
}