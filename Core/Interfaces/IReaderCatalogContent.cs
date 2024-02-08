using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IReaderCatalogContent
    {
        Task<PageDTO<CatalogContentItemDTO>> GetCatalogContent(
            CatalogContentQueryDTO dto,
            string userId,
            CancellationToken cancellationToken
        );
    }
}
