using DCAPP.Services.Contracts;
using DCAPPLIB.Entities.Dtos.User;

namespace DCAPP.Services;

public class AuthService(IHttpClientFactory httpClientFactory) : IAuthService
{
    public async Task<HttpResponseMessage> TryLoginAsync(UserDtoForAuth userLogin)
    {
        var client = httpClientFactory.CreateClient("DentalClinicAPI");
        var result = await client.PostAsJsonAsync($"/api/login", userLogin);

        return result;
    }

    public async Task<HttpResponseMessage> TryRegisterAsync(UserDtoForRegistration userRegister)
    {
        var client = httpClientFactory.CreateClient("DentalClinicAPI");
        var result = await client.PostAsJsonAsync($"/api/auth", userRegister);

        return result;
    }
}