namespace Core.DTOs
{
    public record PageDTO<T>(List<T> Items, long TotalCountItems);

    public record UserDataDTO<T>(T Value, string UserId);
}
