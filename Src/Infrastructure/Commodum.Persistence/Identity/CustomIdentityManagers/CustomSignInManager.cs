using Commodum.Application.Interfaces;
using Commodum.Application.Interfaces.CustomIdentityManagers;
using Commodum.Domain.Entities.Identity;
using Commodum.Domain.Entities.User;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Commodum.Persistence.Identity.CustomIdentityManagers
{
    public class CustomSignInManager : ICustomSignInManager
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDBContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public CustomSignInManager(IHttpContextAccessor contextAccessor,
            SignInManager<ApplicationUser> signInManager,
            IDBContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);

            string loginFailedReason = null;

            if (result.IsNotAllowed)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    loginFailedReason = "Email isn't confirmed.";
                }
                if (!await _userManager.IsPhoneNumberConfirmedAsync(user))
                {
                    loginFailedReason = "Phone Number isn't confirmed.";
                }
            }
            else if (result.IsLockedOut)
            {
                loginFailedReason = "Account is locked out.";
            }
            else if (result.RequiresTwoFactor)
            {
                loginFailedReason = "2FA required.";
            }
            else
            {
                if (user == null)
                {
                    loginFailedReason = "Username is incorrect.";
                }
                else if (!result.Succeeded)
                {
                    loginFailedReason = "Password is incorrect.";
                }
            }

            var appUser = user as ApplicationUser;

            if (appUser != null)
            {
                var ip = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                LoginAudit auditRecord = null;

                switch (result.ToString())
                {
                    case "Succeeded":
                        auditRecord = new LoginAudit { UserId = appUser.Id, IpAddress = ip, RequiresTwoFactor = false, LoginSuccess = true, LoginTime = DateTime.UtcNow, LoginFailedReason = loginFailedReason, TwoFactorLoginSuccess = false, TwoFactorLoginTime = null, TwoFactorLoginFailedReason = null, NoOfRetries = user.AccessFailedCount, LogoutSuccess = false, LogoutTime = null };
                        break;

                    case "Failed":
                        auditRecord = new LoginAudit { UserId = appUser.Id, IpAddress = ip, RequiresTwoFactor = false, LoginSuccess = false, LoginTime = null, LoginFailedReason = loginFailedReason, TwoFactorLoginSuccess = false, TwoFactorLoginTime = null, TwoFactorLoginFailedReason = null, NoOfRetries = user.AccessFailedCount, LogoutSuccess = false, LogoutTime = null };
                        break;

                    case "Lockedout":
                        auditRecord = new LoginAudit { UserId = appUser.Id, IpAddress = ip, RequiresTwoFactor = false, LoginSuccess = false, LoginTime = null, LoginFailedReason = loginFailedReason, TwoFactorLoginSuccess = false, TwoFactorLoginTime = null, TwoFactorLoginFailedReason = null, NoOfRetries = user.AccessFailedCount, LogoutSuccess = false, LogoutTime = null };
                        break;

                    case "NotAllowed":
                        auditRecord = new LoginAudit { UserId = appUser.Id, IpAddress = ip, RequiresTwoFactor = false, LoginSuccess = false, LoginTime = null, LoginFailedReason = loginFailedReason, TwoFactorLoginSuccess = false, TwoFactorLoginTime = null, TwoFactorLoginFailedReason = null, NoOfRetries = user.AccessFailedCount, LogoutSuccess = false, LogoutTime = null };
                        break;

                    case "RequiresTwoFactor":
                        auditRecord = new LoginAudit { UserId = appUser.Id, IpAddress = ip, RequiresTwoFactor = true, LoginSuccess = false, LoginTime = null, LoginFailedReason = loginFailedReason, TwoFactorLoginSuccess = false, TwoFactorLoginTime = null, TwoFactorLoginFailedReason = null, NoOfRetries = user.AccessFailedCount, LogoutSuccess = false, LogoutTime = null };
                        break;
                }
                if (auditRecord != null)
                {
                    var param = new DynamicParameters();

                    param.Add("@UserId", auditRecord.UserId);
                    param.Add("@IpAddress", auditRecord.IpAddress);
                    param.Add("@RequiresTwoFactor", auditRecord.RequiresTwoFactor);
                    param.Add("@LoginSuccess", auditRecord.LoginSuccess);
                    param.Add("@LoginTime", auditRecord.LoginTime);
                    param.Add("@LoginFailedReason", auditRecord.LoginFailedReason);
                    param.Add("@TwoFactorLoginSuccess", auditRecord.TwoFactorLoginSuccess);
                    param.Add("@TwoFactorLoginTime", auditRecord.TwoFactorLoginTime);
                    param.Add("@TwoFactorLoginFailedReason", auditRecord.TwoFactorLoginFailedReason);
                    param.Add("@NoOfRetries", auditRecord.NoOfRetries);
                    param.Add("@LogoutSuccess", auditRecord.LogoutSuccess);
                    param.Add("@LogoutTime", auditRecord.LogoutTime);

                    int newID = _dbContext.QuerySingleOrDefault<int>("Sp_Create_Login_Audit_Log", param, commandType: CommandType.StoredProcedure);
                }
            }
            return result;
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null)
        {
            await _signInManager.SignInAsync(user, isPersistent, authenticationMethod);
        }

        public async Task SignOutAsync(string JwtToken)
        {
            var user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name.ToString()) as ApplicationUser;
            await _signInManager.SignOutAsync();

            if (user != null)
            {
                var param = new DynamicParameters();
                param.Add("@UserId", user.Id);
                param.Add("@JwtToken", JwtToken);

                _dbContext.Execute("Sp_Create_User_Logout_Audit_Log", param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
    

