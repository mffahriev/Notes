using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRepositoryPathCatalog
    {
        Task<PageDTO<CatalogContentItemDTO>> GetContent(
            UserDataDTO<CatalogContentQueryDTO> dto,
            CancellationToken cancellationToken
        );

        Task Insert(
            UserDataDTO<CatalogInsertDTO> dto,
            CancellationToken cancellationToken
        );

        Task Delete(
            UserDataDTO<CatalogItemDTO> dto,
            CancellationToken cancellationToken
        );

        Task Update(
            UserDataDTO<CatalogUpdateDTO> dto,
            CancellationToken cancellationToken
        );
    }
}
