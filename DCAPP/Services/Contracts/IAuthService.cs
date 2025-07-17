using DCAPPLIB.Entities.Dtos.User;

namespace DCAPP.Services.Contracts;

public interface IAuthService
{
    Task<HttpResponseMessage> TryLoginAsync(UserDtoForAuth userLogin);
    Task<HttpResponseMessage> TryRegisterAsync(UserDtoForRegistration userRegister);
}