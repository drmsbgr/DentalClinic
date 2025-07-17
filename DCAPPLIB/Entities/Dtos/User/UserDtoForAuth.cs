using System.ComponentModel.DataAnnotations;

namespace DCAPPLIB.Entities.Dtos.User;

public record UserDtoForAuth
{
    [Required(ErrorMessage = "Kullanıcı adı gereklidir!")]
    public string? UserName { get; init; }
    [Required(ErrorMessage = "Şifre gereklidir!")]
    public string? Password { get; init; }
}