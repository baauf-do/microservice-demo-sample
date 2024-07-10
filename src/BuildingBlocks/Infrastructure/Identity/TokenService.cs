using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Contracts.Identity;
using Microsoft.IdentityModel.Tokens;
using Shared.Configurations;
using Shared.DTOs.Identity;

namespace Infrastructure.Identity
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        /// <summary>
        /// Get token Jwt
        /// </summary>
        /// <param name="request"></param>
        /// <returns>TokenResponse</returns>
        public TokenResponse GetToken(TokenRequest request)
        {
            var token = GenerateJwt();
            var result = new TokenResponse(token);

            return result;
        }

        /// <summary>
        /// Generate Jwt
        /// </summary>
        /// <returns></returns>
        private string GenerateJwt() => GenerateEncryptedToken(GetSigningCredentials());

        /// <summary>
        /// Generate Encrypted Token
        /// </summary>
        /// <param name="signingCredentials"></param>
        /// <returns></returns>
        private static string GenerateEncryptedToken(SigningCredentials signingCredentials)
        {
            var claims = new[]
            {
                new Claim("Role", "Admin")
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Generate Ket Secret
        /// by HmacSha256
        /// </summary>
        /// <returns></returns>
        private SigningCredentials GetSigningCredentials()
        {
            byte[] secret = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            return new SigningCredentials(new SymmetricSecurityKey(secret),
                SecurityAlgorithms.HmacSha256);
        }
    }
}
