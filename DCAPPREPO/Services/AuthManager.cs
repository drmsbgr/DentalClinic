using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos;
using DCAPPLIB.Entities.Dtos.User;
using DCAPPREPO.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DCAPPREPO.Services;

public class AuthManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration) : IAuthService
{
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;
    private User? _user;

    public async Task<TokenDto> CreateTokenAsync(bool populateExp)
    {
        var signInCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signInCredentials, claims);

        var refreshToken = GenerateRefreshToken();
        _user!.RefreshToken = refreshToken;

        if (populateExp)
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

        await _userManager.UpdateAsync(_user);

        var accessToken = new JwtSecurityTokenHandler()
            .WriteToken(tokenOptions);

        return new TokenDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto)
    {
        var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken!);

        var user = await _userManager
            .FindByNameAsync(principal.Identity?.Name!);

        if (user is null ||
            user.RefreshToken != tokenDto.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new SecurityTokenException("Invalid token");
        }
        _user = user;
        return await CreateTokenAsync(false);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["secretKey"];

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings["validIssuer"],
            ValidAudience = jwtSettings["validAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler
                    .ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);


        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken
                .Header
                .Alg
                .Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return principal;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials,
        List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expire"])),
            signingCredentials: signinCredentials);
        return tokenOptions;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]!);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, _user?.UserName!)
        };

        var roles = await _userManager
            .GetRolesAsync(_user!);

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
        return claims;
    }

    public async Task<IdentityResult> RegisterUserAsync(UserDtoForRegistration userDto)
    {
        var user = _mapper.Map<User>(userDto);

        var result = await _userManager.CreateAsync(user, userDto.Password!);

        if (result.Succeeded)
            await _userManager.AddToRolesAsync(user, userDto.Roles!);

        return result;
    }

    public async Task<bool> ValidateUserAsync(UserDtoForAuth userDto)
    {
        Validate(userDto);
        _user = await _userManager.FindByNameAsync(userDto.UserName!);

        var result = _user is not null && await _userManager.CheckPasswordAsync(_user, userDto.Password!);

        return result;
    }

    private static void Validate<T>(T item)
    {
        var vResults = new List<ValidationResult>();
        var context = new ValidationContext(item!);
        var isValid = Validator.TryValidateObject(item!, context, vResults, true);

        if (!isValid)
        {
            var errors = string.Join(" ", vResults.Select(v => v.ErrorMessage));
            throw new ValidationException(errors);
        }
    }
}