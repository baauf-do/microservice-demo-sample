using AdminApp.Models;
using IdentityModel.Client;

namespace AdminApp.Services.Interfaces;

public interface IAuthService
{
    Task<TokenResponse> Login(LoginRequest loginRequest);
    Task Logout();
}
