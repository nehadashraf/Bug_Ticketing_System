using BugTicketingSystem.BL.DTOs;

namespace BugTicketingSystem.BL.Mangers.Users
{
   public interface IUserManager
    {
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<IEnumerable<UserListDto>> GetAllUsersAsync();
    }
}
