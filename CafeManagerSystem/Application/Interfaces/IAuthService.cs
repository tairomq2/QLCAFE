using Application.DTOs.User;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseApi<AuthResponse>> LoginAsync(UserLoginDTO user);
    }
}
