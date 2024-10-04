
using FirebaseAdmin.Auth;
using TaggerApi.DTOs;

namespace TaggerApi.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public async Task<UserRecord> getUser(string uid)
    {
        return await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
    }

    public async Task<string> RegisterAsync(UserRegisterDTO userRegister)
    {
        var userArgs = new UserRecordArgs{
           Email = userRegister.Email,
           Password = userRegister.Password
        };    

        var usuario = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

        return usuario.Uid;
    }

    
}