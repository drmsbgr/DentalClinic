using DCAPPLIB.Entities.Dtos;
using DCAPPLIB.Entities.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace DCAPPLIB.Services.Contracts;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(UserDtoForRegistration userDto);
    Task<bool> ValidateUserAsync(UserDtoForAuth userDto);
    Task<TokenDto> CreateTokenAsync(bool populateExp);
    Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto);
}