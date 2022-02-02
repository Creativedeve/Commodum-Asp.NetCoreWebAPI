using Commodum.Application.Interfaces;
using Commodum.Application.Interfaces.CustomIdentityManagers;
using Commodum.Domain.Entities.Identity;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Commodum.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Commodum.Persistence.Identity.CustomIdentityManagers
{
    public class CustomUserManager : ICustomUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDBContext _dbContext;
        private readonly TokenManagement _tokenManagement;

        public CustomUserManager(UserManager<ApplicationUser> userManager, IDBContext dbContext, IOptions<TokenManagement> tokenManagement)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _tokenManagement = tokenManagement.Value;
        } 
        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var userToVerify = await _userManager.FindByNameAsync(userName);
            return userToVerify;
        }

        public string GenerateJwtToken(ApplicationUser user, bool? IsTwoFA = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenManagement.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenSecure = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tokenSecure);

            if (token != null)
            {
                var param = new DynamicParameters();
                param.Add("@UserId", user.Id);
                param.Add("@UserLoginTokenType", TokenTypes.JwtToken.ToString());
                param.Add("@TokenValue", token);
                param.Add("@IsTwoFA", IsTwoFA);

                _dbContext.Execute("Sp_User_Login_Token_Create", param, commandType: CommandType.StoredProcedure);
            }

            return token;
        }

        public async Task<bool> VerifyTwoFactorTokenAsync(ApplicationUser user, string tokenProvider, string token)
        {
            var result = await _userManager.VerifyTwoFactorTokenAsync(user, tokenProvider, token);

            return result;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<ApplicationUser> FindByIdAsync(int userId)
        {
            var result = await _userManager.FindByIdAsync(userId.ToString());

            return result;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);

            return result;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            var result = await _userManager.GeneratePasswordResetTokenAsync(user);
            return result;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            var result = await _userManager.SetTwoFactorEnabledAsync(user, enabled);
            return result;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var result = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return result;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }

        public async Task<string> GenerateChangePhoneNumberTokenAsync(ApplicationUser user, string phoneNumber)
        {
            var result = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
            return result;
        }

        public async Task<IdentityResult> ChangePhoneNumberAsync(ApplicationUser user, string phoneNumber, string token)
        {
            var result = await _userManager.ChangePhoneNumberAsync(user, phoneNumber, token);
            return result;
        }

        public async Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            var result = await _userManager.HasPasswordAsync(user);
            return result;
        }

        public async Task<IdentityResult> RemovePasswordAsync(ApplicationUser user)
        {
            var result = await _userManager.RemovePasswordAsync(user);
            return result;
        }

        public async Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.AddPasswordAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result;
        }
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return
             _userManager.GetRolesAsync(user);
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            return _userManager.DeleteAsync(user);
        }

        public Task<IdentityResult> UpdateSecurityStampAsync(ApplicationUser user)
        {
            return _userManager.UpdateSecurityStampAsync(user);
        }

        public Task<IdentityResult> ResetAccessFailedCountAsync(ApplicationUser user)
        {
            return _userManager.ResetAccessFailedCountAsync(user);
        }

        public Task<IdentityResult> AccessFailedAsync(ApplicationUser user)
        {
            return _userManager.AccessFailedAsync(user);
        }
    }
}

