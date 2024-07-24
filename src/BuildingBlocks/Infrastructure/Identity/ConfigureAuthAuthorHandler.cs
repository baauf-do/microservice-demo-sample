using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using IdentityServer4.AccessTokenValidation;

namespace Infrastructure.Identity
{
    public static class ConfigureAuthAuthorHandler
    {
        /// <summary>
        /// get config in file setting ... .json
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="Exception"></exception>
        public static void ConfigureAuthenticationHandler(this IServiceCollection services)
        {
            var configuration = services.GetOptions<ApiConfiguration>("ApiConfiguration");
            if (configuration == null || string.IsNullOrEmpty(configuration.IssuerUri) ||
                string.IsNullOrEmpty(configuration.ApiName)) throw new Exception("ApiConfiguration is not configured!");

            var issuerUri = configuration.IssuerUri;
            var apiName = configuration.ApiName;
            //var authScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;  // bien constant "Bearer"
            #region not using => because not support -> change to JwtBearer
            // services.AddAuthentication(options =>
            //     {
            //         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     })
            //     .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, opt =>
            //     {
            //         opt.Authority = issuerUri; // xac thuc token - on docker no chinh la container name
            //         opt.ApiName = apiName; // ten api su dung -> // cau hinh o ben IdentityServer
            //         opt.RequireHttpsMetadata = false; // yeu cau https ?
            //         opt.SupportedTokens = SupportedTokens.Both; // cho ca 2 JWT/Reference
            //     })
            // ;
            #endregion

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = issuerUri; //identityUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = apiName; //"exam_api"; // ten api su dung -> // cau hinh o ben IdentityServer
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                })
            ;
        }

        //public static void ConfigureAuthorization(this IServiceCollection services)
        /// <summary>
        /// Configure Authorization
        /// [
        /// Include:
        /// - AddPolicy
        /// ]
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAuthorizationHandler(this IServiceCollection services)
        {
            services.AddAuthorization(
                options =>
                {
                    options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
                    {
                        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireAuthenticatedUser();
                    });
                });
        }
    }
}
