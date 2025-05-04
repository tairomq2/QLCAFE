using Application.DTOs.User;
using Application.Interfaces;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddNewUser([FromBody] CreateUserDTO createUserDTO)
        {
           var response = await _userService.CreateUserAsync(createUserDTO);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
