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

    public record CatalogItemDTO(string FullPath);

    public record CatalogInsertDTO(string BasePath, string Name);

    public record CatalogUpdateDTO(string SourceBasePath, string? TargetBasePath, string? NewName);
}
