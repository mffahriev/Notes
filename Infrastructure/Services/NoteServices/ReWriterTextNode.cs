using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.NoteServices
{
    public class ReWriterTextNode : IReWriterTextNote
    {
        private readonly IPathManager<Note> _pathManager;
        private readonly ApplicationContext _context;

        public ReWriterTextNode(
            IPathManager<Note> pathManager, 
            ApplicationContext context
        )
        {
            _context = context;
            _pathManager = pathManager;
        }

        public async Task ReWriteText(UserDataDTO<NoteUpdateContentDTO> dto, CancellationToken cancellationToken)
        {
            Note note = await _pathManager.Get(dto.Value.FullPath, dto.UserId, cancellationToken);

            await _context.Notes
                .Where(x => x.Id == note.Id)
                .ExecuteUpdateAsync(
                    setter => setter
                        .SetProperty(x => x.Text, dto.Value.Text)
                        .SetProperty(x => x.LastUpdateText, DateTimeOffset.Now),
                    cancellationToken
                );
        }
    }
}
