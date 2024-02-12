using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.NoteServices
{
    public class RepositoryPathNoteService : IRepositoryPathNote
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Note> _repository;
        private readonly IPathManager<Catalog> _pathCategoryManager;
        private readonly IPathManager<Note> _pathNoteManager;

        public RepositoryPathNoteService(
            UserManager<User> userManager,
            IRepository<Note> repository,
            IPathManager<Catalog> pathManager,
            IPathManager<Note> pathNoteManager
        )
        {
            _repository = repository;
            _pathCategoryManager = pathManager;
            _userManager = userManager;
            _pathNoteManager = pathNoteManager;
        }

        public async Task Delete(UserDataDTO<NoteQueryDTO> dto, CancellationToken cancellationToken)
        {
            Note note = await _pathNoteManager.Get(
                dto.Value.FullPath, 
                dto.UserId, 
                cancellationToken
            );
            await _repository.Delete(note, cancellationToken);
        }

        public async Task<NoteContentDTO> GetContent(UserDataDTO<NoteQueryDTO> dto, CancellationToken cancellationToken)
        {
            Note note = await _pathNoteManager.Get(
                dto.Value.FullPath, 
                dto.UserId, 
                cancellationToken);
            return new NoteContentDTO(note.Text);
        }

        public async Task Insert(UserDataDTO<NoteInsertDTO> dto, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(dto.UserId) ?? throw new NotImplementedException();

            Catalog catalog = await _pathCategoryManager.Get(
                dto.Value.BaseCatalog, 
                dto.UserId, 
                cancellationToken
            );

            await _repository.Insert(
                new Note(dto.Value.Name, dto.Value.Text, catalog, user),
                cancellationToken
            );
        }

        public async Task Update(UserDataDTO<NoteUpdateDTO> dto, CancellationToken cancellationToken)
        {
            Note note = await _pathNoteManager.Get(dto.Value.SourceFullPath, dto.UserId, cancellationToken);

            if (!string.IsNullOrWhiteSpace(dto.Value.TargetBasePath))
            {
                Catalog targetCatalog = await _pathCategoryManager.Get(
                    dto.Value.TargetBasePath, 
                    dto.UserId, 
                    cancellationToken
                );

                note.Catalog = targetCatalog;
            }

            if (!string.IsNullOrEmpty(dto.Value.Name))
            {
                note.Name = dto.Value.Name;
            }

            await _repository.Update(note, cancellationToken);
        }
    }
}
