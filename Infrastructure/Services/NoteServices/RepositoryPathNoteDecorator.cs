using Core.DTOs;
using Core.Interfaces;
using FluentValidation;

namespace Infrastructure.Services.NoteServices
{
    public class RepositoryPathNoteDecorator : IRepositoryPathNote
    {
        private readonly IRepositoryPathNote _repositoryPathNoteService;
        private readonly IValidator<UserDataDTO<NoteQueryDTO>> _validatorNoteQuery;
        private readonly IValidator<UserDataDTO<NoteInsertDTO>> _validatorNoteInsert;
        private readonly IValidator<UserDataDTO<NoteUpdateDTO>> _validatorUpdate;
            
        public RepositoryPathNoteDecorator(
            IRepositoryPathNote repositoryPathNoteService,
            IValidator<UserDataDTO<NoteQueryDTO>> validatorNoteQuery,
            IValidator<UserDataDTO<NoteInsertDTO>> validatorNoteInsert,
            IValidator<UserDataDTO<NoteUpdateDTO>> validatorUpdate
        ) 
        {
            _repositoryPathNoteService = repositoryPathNoteService;
            _validatorNoteQuery = validatorNoteQuery;
            _validatorNoteInsert = validatorNoteInsert;
            _validatorUpdate = validatorUpdate;
        }

        public async Task Delete(UserDataDTO<NoteQueryDTO> dto, CancellationToken cancellationToken)
        {
            await _validatorNoteQuery.ValidateAndThrowAsync(dto, cancellationToken);
            await _repositoryPathNoteService.Delete(dto, cancellationToken);
        }

        public async Task<NoteContentDTO> GetContent(UserDataDTO<NoteQueryDTO> dto, CancellationToken cancellationToken)
        {
            await _validatorNoteQuery.ValidateAndThrowAsync(dto, cancellationToken);
            return await _repositoryPathNoteService.GetContent(dto, cancellationToken);
        }

        public async Task Insert(UserDataDTO<NoteInsertDTO> dto, CancellationToken cancellationToken)
        {
            await _validatorNoteInsert.ValidateAndThrowAsync(dto, cancellationToken);
            await _repositoryPathNoteService.Insert(dto, cancellationToken);
        }

        public async Task Update(UserDataDTO<NoteUpdateDTO> dto, CancellationToken cancellationToken)
        {
            await _validatorUpdate.ValidateAndThrowAsync(dto, cancellationToken);
            await _repositoryPathNoteService.Update(dto, cancellationToken);
        }
    }
}
