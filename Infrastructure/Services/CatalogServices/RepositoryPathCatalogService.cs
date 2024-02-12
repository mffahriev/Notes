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
    public class RepositoryPathCatalogService : IRepositoryPathCatalog
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Catalog> _repository;
        private readonly IPathManager<Catalog> _pathManager;
        private readonly IMapper _mapper;

        public RepositoryPathCatalogService(
            ApplicationContext context,
            UserManager<User> userManager,
            IRepository<Catalog> repository,
            IPathManager<Catalog> pathManager,
            IMapper mapper
        )
        {
            _repository = repository;
            _pathManager = pathManager;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PageDTO<CatalogContentItemDTO>> GetContent(
            UserDataDTO<CatalogContentQueryDTO> dto,
            CancellationToken cancellationToken
        )
        {
            Catalog catalog = await _pathManager.Get(
               dto.Value.FullPath ?? throw new InvalidOperationException(),
               dto.UserId,
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
                    ((dto.Value.PageNumber ?? throw new InvalidOperationException()) - 1)
                    * (dto.Value.PageSize ?? throw new InvalidOperationException())
                ).Take(dto.Value.PageSize ?? throw new InvalidOperationException());

            return new PageDTO<CatalogContentItemDTO>
                (
                    await queryItems.ToListAsync(cancellationToken),
                    await queryTotalItems.LongCountAsync()
                );
        }

        public async Task Delete(UserDataDTO<CatalogItemDTO> dto,CancellationToken cancellationToken)
        {
            Catalog catalog = await _pathManager.Get(
                dto.Value.FullPath, 
                dto.UserId, 
                cancellationToken
            );

            await _repository.Delete(catalog, cancellationToken);
        }

        public async Task Insert(UserDataDTO<CatalogInsertDTO> dto, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(dto.UserId) ?? throw new NotImplementedException();

            Catalog catalog = await _pathManager.Get(
                dto.Value.BasePath,
                user.Id, 
                cancellationToken
            );

            await _repository.Insert(
                new Catalog(dto.Value.Name, user, catalog), 
                cancellationToken
            );
        }

        public async Task Update(UserDataDTO<CatalogUpdateDTO> dto, CancellationToken cancellationToken)
        {
            Catalog catalog = await _pathManager.Get(
                dto.Value.SourceBasePath,
                dto.UserId,
                cancellationToken
            );

            if (!string.IsNullOrWhiteSpace(dto.Value.TargetBasePath))
            {
                catalog.ParentCatalog = await _pathManager.Get(
                    dto.Value.TargetBasePath, 
                    dto.UserId, 
                    cancellationToken
                );
            }

            if (!string.IsNullOrWhiteSpace(dto.Value.NewName))
            {
                catalog.Name = dto.Value.NewName;
            }

            await _repository.Update(catalog, cancellationToken);
        }
    }
}
