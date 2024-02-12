using Core.Entities;
using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CatalogServices
{
    public class PathManagerCatalog : IPathManager<Catalog?>
    {
        private readonly ApplicationContext _context;

        public PathManagerCatalog(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Catalog?> Get(string fullPath, string userId, CancellationToken cancellationToken)
        {
            string[] catalogs = fullPath.Split(Path.DirectorySeparatorChar);

            Catalog headCatalog = await _context.Catalogs
                .AsNoTracking()
                .SingleAsync(
                    x => x.Name == catalogs.First()
                    && x.User.Id == userId,
                    cancellationToken
                );

            for (int i = 0; i < catalogs.Length; i++)
            {
                headCatalog = headCatalog.ChildrenCatalogs.Single(
                    x => x.Name == catalogs[i]
                    && x.User.Id == userId
                );
            }

            if (headCatalog.Name == catalogs.Last())
            {
                return headCatalog;
            }

            return null;
        }
    }
}