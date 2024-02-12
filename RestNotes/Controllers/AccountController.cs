using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthorization _authorizationService;
        private readonly IRegistration _registrationService;

        public AccountController(
            IRegistration registrationService,
            IAuthorization authorizationService
        )
        {
            _registrationService = registrationService;
            _authorizationService = authorizationService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registration([FromBody] RegisterUserDTO dto)
        {
             await _registrationService.Registration(dto);
            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            TokenDTO tokenDTO = await _authorizationService.GetAccessToken(dto);
            return Ok(tokenDTO);
        }
    }
}
