using Core.Entities;
using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.NoteServices
{
    public class NoteRepository : IRepository<Note>
    {
        private readonly ApplicationContext _context;

        public NoteRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Delete(Note entity, CancellationToken token)
        {
            await _context
                .Notes
                .Where(x => x.Id == entity.Id)
                .ExecuteDeleteAsync(token);
        }

        public async Task<Note> Get(Guid Id, CancellationToken token)
        {
            return await _context
                .Notes
                .SingleAsync(x => x.Id == Id, token);
        }

        public async Task Insert(Note entity, CancellationToken token)
        {
            _context.Notes.Add(entity);
            await _context.SaveChangesAsync(token);
        }

        public async Task Update(Note entity, CancellationToken token)
        {
            await _context.Notes
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(
                    setter => setter
                        .SetProperty(x => x.Name, entity.Name)
                        .SetProperty(x => x.Catalog, entity.Catalog)
                        .SetProperty(x => x.Updated, DateTimeOffset.Now),
                    token
                );
        }
    }
}
