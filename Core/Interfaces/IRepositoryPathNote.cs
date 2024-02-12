using Core.DTOs;

namespace Core.Interfaces
{
    public interface IRepositoryPathNote
    {
        Task<NoteContentDTO> GetContent(
            UserDataDTO<NoteQueryDTO> dto,
            CancellationToken cancellationToken
        );

        Task Insert(
            UserDataDTO<NoteInsertDTO> dto,
            CancellationToken cancellationToken
        );

        Task Delete(
            UserDataDTO<NoteQueryDTO> dto,
            CancellationToken cancellationToken
        );

        Task Update(
            UserDataDTO<NoteUpdateDTO> dto,
            CancellationToken cancellationToken
        );
    }
}
