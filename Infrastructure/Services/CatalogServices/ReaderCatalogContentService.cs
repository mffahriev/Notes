using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CatalogServices
{
    public class ReaderCatalogContentService : IReaderCatalogContent
    {
        private readonly IGetterCatalog _getterCatalog;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ReaderCatalogContentService(
            IGetterCatalog getterCatalog, 
            ApplicationContext context, 
            IMapper mapper
        )
        {
            _getterCatalog = getterCatalog;
            _context = context;
            _mapper = mapper;
        }

        public async Task<PageDTO<CatalogContentItemDTO>> GetCatalogContent(
            CatalogContentQueryDTO dto,
            string userId,
            CancellationToken cancellationToken
        )
        {
             Catalog catalog = await _getterCatalog.GetCatalog(
                dto.FullPath ?? throw new InvalidOperationException(),
                userId, 
                cancellationToken
            );

            IQueryable<CatalogContentItemDTO> queryTotalItems = _context
                .Catalogs
                .AsNoTracking()
                .Where(x => x.ParentCatalog == catalog)
                .ProjectTo<CatalogContentItemDTO>(_mapper.ConfigurationProvider)
                .Union(_context.Notes.AsNoTracking()
                .Where(x => x.Catalog == catalog)
                .ProjectTo<CatalogContentItemDTO>(_mapper.ConfigurationProvider));

            IQueryable<CatalogContentItemDTO> queryItems = queryTotalItems
                .OrderBy(x => x.Name)
                .Skip(
                    ((dto.PageNumber ?? throw new InvalidOperationException()) - 1)
                    * (dto.PageSize ?? throw new InvalidOperationException())
                ).Take(dto.PageSize ?? throw new InvalidOperationException());

            return new PageDTO<CatalogContentItemDTO>
                (
                    await queryItems.ToListAsync(cancellationToken),
                    await queryTotalItems.LongCountAsync()
                );
        }
    }
}
