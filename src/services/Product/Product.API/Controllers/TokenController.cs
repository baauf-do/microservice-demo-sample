using Contracts.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Identity;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/product/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetToken()
        {
            var result = _tokenService.GetToken(new TokenRequest());

            return Ok(result);
        }
    }
}
