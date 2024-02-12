using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RestNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly IRepositoryPathNote _repository;
        private readonly IReWriterTextNote _rewriter;

        public NoteController(
            IRepositoryPathNote repository, 
            IReWriterTextNote rewriter
        ) 
        {
            _repository = repository;
            _rewriter = rewriter;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromBody] NoteQueryDTO dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
            NoteContentDTO result = await _repository.GetContent(
                new UserDataDTO<NoteQueryDTO>(dto, userId), 
                HttpContext.RequestAborted
            );

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(NoteInsertDTO dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
            await _repository.Insert(new UserDataDTO<NoteInsertDTO>(dto, userId), HttpContext.RequestAborted);

            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(NoteUpdateDTO dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
            await _repository.Update(new UserDataDTO<NoteUpdateDTO>(dto, userId), HttpContext.RequestAborted);

            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateContent(NoteUpdateContentDTO dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
            await _rewriter.ReWriteText(new UserDataDTO<NoteUpdateContentDTO>(dto, userId), HttpContext.RequestAborted);

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(NoteQueryDTO dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
            await _repository.Delete(new UserDataDTO<NoteQueryDTO>(dto, userId), HttpContext.RequestAborted);

            return Ok();
        }
    }
}
