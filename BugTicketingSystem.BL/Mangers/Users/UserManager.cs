using BugTicketingSystem.BL.DTOs;
using BugTicketingSystem.BL.Mangers.Users;
using BugTicketingSystem.DAL.Models;
using BugTicketingSystem.DAL.Services;
using Microsoft.AspNetCore.Identity;

namespace BugTicketingSystem.BL.Managers
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public UserManager(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await _userManager.FindByNameAsync(registerDto.UserName) != null)
            {
                throw new InvalidOperationException("Username is already taken");
            }


            var user = new User
            {
                UserName = registerDto.UserName.ToLower(),
                Roles = registerDto.Roles.Select(role => role.ToLower()).ToList() 
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Registration failed: {errors}");
            }
               
            return new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles=user.Roles,
                Token = await _tokenService.CreateToken(user)
            };
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName.ToLower());
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid password");
            }

            return new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = user.Roles,
                Token = await _tokenService.CreateToken(user)
            };
        }

        public async Task<IEnumerable<UserListDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<UserListDto>();

            foreach (var user in users)
            {

                userDtos.Add(new UserListDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = user.Roles
                });
            }

            return userDtos;
        }
    }
}