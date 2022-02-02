using Commodum.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commodum.Application.Interfaces.CustomIdentityManagers
{
    public interface ICustomUserManager
    {
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<ApplicationUser> FindByIdAsync(int userId);
        Task<ApplicationUser> FindByEmailAsync(string email);
        string GenerateJwtToken(ApplicationUser user, bool? IsTwoFA);
        Task<bool> VerifyTwoFactorTokenAsync(ApplicationUser user, string tokenProvider, string token);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
        Task<string> GenerateChangePhoneNumberTokenAsync(ApplicationUser user, string phoneNumber);
        Task<IdentityResult> ChangePhoneNumberAsync(ApplicationUser user, string phoneNumber, string token);
        Task<bool> HasPasswordAsync(ApplicationUser user);
        Task<IdentityResult> RemovePasswordAsync(ApplicationUser user);
        Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        Task<IdentityResult> UpdateSecurityStampAsync(ApplicationUser user);
        Task<IdentityResult> ResetAccessFailedCountAsync(ApplicationUser user);
        Task<IdentityResult> AccessFailedAsync(ApplicationUser user);
    }
}
