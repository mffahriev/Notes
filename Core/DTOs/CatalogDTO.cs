using Core.Enums;

namespace Core.DTOs
{
    public record CatalogContentQueryDTO(string? FullPath, int? PageNumber, int? PageSize);

    public record CatalogContentItemDTO(
        string FullPath, 
        string Name, 
        CategoryContentEnum Type, 
        DateTimeOffset Created, 
        DateTimeOffset? Updated
    );
}
