using Core.DTOs;

namespace Core.Interfaces
{
    public interface IReWriterTextNote
    {
        Task ReWriteText(UserDataDTO<NoteUpdateContentDTO> dto, CancellationToken cancellationToken);
    }
}
