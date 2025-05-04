using Application.DTOs.User;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService 
    {
        Task<ResponseApi<List<UserDTO>>> GetAllUsersAsync();
        Task<ResponseApi<UserDTO>> CreateUserAsync(CreateUserDTO userDTO);
        Task <ResponseApi<bool>> UpdateUserAsync(UserDTO userDTO);
        Task <ResponseApi<bool>> DeleteUserAsync(int id);

    }
}
