using Core.Entities;
using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CatalogServices
{
    public class CatalogRepository : IRepository<Catalog>
    {
        private readonly ApplicationContext _context;

        public CatalogRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Delete(Catalog entity, CancellationToken token)
        {
            await _context
                .Catalogs
                .Where(x => x.Id == entity.Id)
                .ExecuteDeleteAsync(token);
        }

        public async Task<Catalog> Get(Guid Id, CancellationToken token)
        {
            return await _context
                .Catalogs
                .SingleAsync(x => x.Id == Id, token);
        }

        public async Task Insert(Catalog entity, CancellationToken token)
        {
            _context.Catalogs.Add(entity);
            await _context.SaveChangesAsync(token);
        }

        public async Task Update(Catalog entity, CancellationToken token)
        {
            await _context.Catalogs
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(
                    setter => setter.SetProperty(x => x.ParentCatalog, entity.ParentCatalog)
                                    .SetProperty(x => x.Name, entity.Name)
                                    .SetProperty(x => x.Updated, DateTimeOffset.Now),
                    token
                );
        }
    }
}
