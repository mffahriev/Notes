namespace Core.DTOs
{
    public record RegisterUserDTO(string Name, string Email, string Password, string ConfirmPassword);

    public record LoginUserDTO(string Email, string Password);

    public record TokenDTO(string AccessToken);
}
