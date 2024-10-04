namespace TaggerApi.Services.Authentication;

using FirebaseAdmin.Auth;
using TaggerApi.DTOs;
public interface IAuthenticationService{
    Task<string> RegisterAsync(UserRegisterDTO userRegister);

    Task<UserRecord> getUser(string uid);
}