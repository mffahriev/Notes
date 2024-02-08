namespace Core.DTOs
{
    public record PageDTO<T>(List<T> Items, long TotalCountItems);
}
