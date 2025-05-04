using Application.DTOs.User;
using Application.Interfaces;
using Application.Models;
using Core.Common;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public UserRoleDTO MapUserRoleToUserRoleDTO(UserRole userRole)
        {
            return new UserRoleDTO
            {
                RoleId = userRole.RoleID,
                RoleName = userRole.RoleName,
            };
        }
        public UserDTO MapUserToUserDTO(User user, UserRole userRole)
        {
            return new UserDTO
            {
                UserId = user.UserID,
                EmployeeId = user.EmployeeID,
                Username = user.Username,
                Password = user.PasswordHash,
                Role = MapUserRoleToUserRoleDTO(userRole),
                LastLogin = user.LastLogin
            };
        }

        public async Task<ResponseApi<UserDTO>> CreateUserAsync(CreateUserDTO userDTO)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                var existUser = await _unitOfWork.Users.ExistUserAsync(userDTO.EmployeeId);
                if (existUser)
                {
                    return new ResponseApi<UserDTO> { Message = $"User với Id {userDTO.EmployeeId} đã tồn tại" };
                }
                var employee = await _unitOfWork.Employees.GetByIdAsync(userDTO.EmployeeId);
                if (employee == null)
                {
                    return new ResponseApi<UserDTO> { Message = "Employee not found" };
                }
                
                var role = await _unitOfWork.UserRoles.GetRoleNameByPosition(employee.Position);
                if (role == null) {
                    return new ResponseApi<UserDTO> { Message = "User role not found" };
                }
                var user = new User(userDTO.EmployeeId, userDTO.Username, userDTO.Password,role.RoleID );
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new ResponseApi<UserDTO>
                {
                    Success = true,
                    Message = "User created successfully",
                    Data = MapUserToUserDTO(user, new UserRole
                    {
                        RoleID = role.RoleID,
                        RoleName = role.RoleName
                    })
                };
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ResponseApi<UserDTO> { Message = ex.Message };
            }
        }

        public Task<ResponseApi<bool>> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseApi<List<UserDTO>>> GetAllUsersAsync()
        {
            var user = await _unitOfWork.Users.GetAllAsync();
            var userRole = await _unitOfWork.UserRoles.GetAllAsync();
            var roleDictionary = userRole.ToDictionary(r => r.RoleID);

            var userDTOs = new List<UserDTO>();
            foreach (var u in user)
            {
                if (!roleDictionary.TryGetValue(u.RoleID, out var role))
                    return new ResponseApi<List<UserDTO>> { Message = $"Không tìm thấy Role cho user {u.Username}" };
                userDTOs.Add(MapUserToUserDTO(u, role));
            }
            return new ResponseApi<List<UserDTO>>
            {
                Success = true,
                Message = "Get all users successfully",
                Data = userDTOs
            };
        }

        public Task<ResponseApi<bool>> UpdateUserAsync(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApi<UserDTO>> CreateUserAsync(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
