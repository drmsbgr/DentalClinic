using System.ComponentModel.DataAnnotations;

namespace DCAPPLIB.Entities.Dtos.User;

public record UserDtoForRegistration
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    [Required(ErrorMessage = "Kullanıcı adı gereklidir!")]
    public string? UserName { get; init; }
    [Required(ErrorMessage = "Şifre gereklidir!")]
    public string? Password { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public ICollection<string>? Roles { get; init; }
}

public record UserDtoForAuth
{
    [Required(ErrorMessage = "Kullanıcı adı gereklidir!")]
    public string? UserName { get; init; }
    [Required(ErrorMessage = "Şifre gereklidir!")]
    public string? Password { get; init; }
}