using Application.DTOs.User;
using Application.Helpers;
using Application.Interfaces;
using Application.Models;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        public AuthService(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }
        public async Task<ResponseApi<AuthResponse>> LoginAsync(UserLoginDTO user)
        {
            await _unitOfWork.BeginTranSacationAsync();
            try
            {
                var userExist = await _unitOfWork.Users.GetUserByUsernameAsync(user.Username);
                if (userExist == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ReponseHelper.NotFound<AuthResponse>("User not exist");
                }
                if (user.Password != userExist.PasswordHash)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ReponseHelper.Fail<AuthResponse>("Password does not match");
                }
                var token = await _jwtProvider.GenerateJwtToken(userExist);
                await _unitOfWork.CommitTransactionAsync();
                return ReponseHelper.Success(token,"Login successful");
            }catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ReponseHelper.ServerError<AuthResponse>(ex.Message);
            }
          
        }

    }
}
