
using FirebaseAdmin.Auth;
using TaggerApi.DTOs;
using TaggerApi.Models;

namespace TaggerApi.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{

    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient){
        _httpClient = httpClient;
    }

    public async Task<UserRecord> getUser(string uid)
    {
        return await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
    }

    public async Task<string> LoginAsync(LoginDTO request)
    {
        var credentials = new {
            request.Email,
            request.Password,
            returnSecureToken= true
        };

        var response = await _httpClient.PostAsJsonAsync("", credentials);

        var authFirebaseObject = await response.Content.ReadFromJsonAsync<AuthFirebase>();
        
        return authFirebaseObject!.IdToken!;
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