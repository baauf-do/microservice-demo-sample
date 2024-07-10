using Shared.DTOs.Identity;

namespace Contracts.Identity
{
    public interface ITokenService
    {
        /// <summary>
        /// Get token Jwt
        /// </summary>
        /// <param name="request"></param>
        /// <returns>TokenResponse</returns>
        TokenResponse GetToken(TokenRequest request);
    }
}
