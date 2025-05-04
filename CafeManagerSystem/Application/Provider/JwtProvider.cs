using Application.Interfaces;
using Application.Models;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Provider
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtSetting _setting;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtProvider(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _setting = new JwtSetting
            {
                Key = configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key not found!"),
                Issuer = configuration["Jwt:Issuer"] ?? "UnknownIssuer",
                Audience = configuration["Jwt:Audience"] ?? "UnknownAudience"
            };
            _httpContextAccessor = contextAccessor;
        }

        public Task<AuthResponse> GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_setting.Key);

            // 🔥 Lấy RoleName từ phương thức GetRoleName()
            string roleName = user.GetRoleName();

            // 🔥 Tạo danh sách Claims (thông tin người dùng)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, roleName) // ⚡ Lưu Role theo tên thay vì ID
            };

            // 🔥 Tạo Access Token (hết hạn sau 2 giờ)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2), // ⏳ Hết hạn sau 2 giờ
                Issuer = _setting.Issuer,
                Audience = _setting.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            // 🔥 Tạo Refresh Token (hết hạn sau 7 ngày)
            var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            // ✅ Lưu Refresh Token vào Cookie
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true, // 🔐 Chống XSS
                Secure = true,   // 🔒 Chỉ gửi qua HTTPS
                SameSite = SameSiteMode.Strict, // 🛡 Chống CSRF
                Expires = DateTime.UtcNow.AddDays(7) // ⏳ Refresh Token có hạn 7 ngày
            });

            // ✅ Trả về AuthResponse chứa AccessToken & RefreshToken
            var authResponse = new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return Task.FromResult(authResponse);
        }
    }
}
