using Commodum.Application.Interfaces;
using Commodum.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commodum.Persistence.Identity.DapperTables
{
    public class DapperUsersTable
    {

        private readonly IDBContext _context;
        public DapperUsersTable(IDBContext context)
        {
            _context = context;
        }

        #region createuser

        public IdentityResult CreateAsync(ApplicationUser user)
        {
            string sql = $@"INSERT INTO AspNetUsers (Guid,UserName, NormalizedUserName,UserPNumber, Email,
                    NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp , ConcurrencyStamp  , PhoneNumber, 
					PhoneNumberConfirmed, TwoFactorEnabled , LockoutEnabled , AccessFailedCount,FirstName,LastName,PersonalIdNumber,
                    DOB,Address,ZipCode,State,UserType
                    )
                    VALUES 
					(		   
					@Guid,@UserName,@SponsorShipRef, @NormalizedUserName, @Email,
                    @NormalizedEmail, @EmailConfirmed,
					@PasswordHash,	   
					@SecurityStamp,
					@ConcurrencyStamp,
                    @PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled,
					@LockoutEnabled,
					@AccessFailedCount,
                    @FirstName,
                    @LastName,
                    @PersonalIdNumber,
                    @DOB,
                    @Address,
                    @ZipCode,
                    @State,
                    @UserType
					);
                    ";

            int rows = _context.Execute(sql, user);

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." });
        }
        #endregion

        public IdentityResult DeleteAsync(ApplicationUser user)
        {
            string sql = $"DELETE FROM AspNetUsers WHERE Id = @Id";
            int rows = _context.Execute(sql, new { user.Id });

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not delete user {user.Email}." });
        }

        public ApplicationUser FindByIdAsync(string userId)
        {
            string sql = $@"SELECT Id,
                            UserName,
                            NormalizedUserName,
                            Email,
                            NormalizedEmail,
                            EmailConfirmed,
                            PasswordHash,
                            SecurityStamp,
                            ConcurrencyStamp,
                            PhoneNumber,
                            PhoneNumberConfirmed,
                            TwoFactorEnabled,
                            LockoutEnd,
                            LockoutEnabled,
                            AccessFailedCount,
						FROM AspNetUsers 
						WHERE Id = @Id;";

            return _context.QuerySingleOrDefault<ApplicationUser>(sql, new
            {
                Id = userId
            });
        }

        public IdentityResult RemoveFromRoleAsync(string id, string role)
        {
            int rows = _context.Execute($@"delete from AspNetUserRoles WHERE UserId = @Id", new
            {
                Id = id
            });
            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not remove user role." });
        }
        public ApplicationUser FindByNameAsync(string userName)
        {
            string sql = $@"SELECT Id,
                            UserName,
                            NormalizedUserName,
                            UserPNumber,
                            Email,
                            FirstName,
                            LastName,
                            Address,
                            ZipCode,
                            State,
                            PersonalIdNumber,
                            NormalizedEmail,
                            EmailConfirmed,
                            PasswordHash,
                            SecurityStamp,
                            ConcurrencyStamp,
                            PhoneNumber,
                            PhoneNumberConfirmed,
                            TwoFactorEnabled,
                            LockoutEnd,
                            LockoutEnabled,
                            AccessFailedCount
                        FROM AspNetUsers
                        WHERE UserName = @UserName;";

            return _context.QuerySingleOrDefault<ApplicationUser>(sql, new
            {
                UserName = userName
            });
        }

        public IdentityResult UpdateAsync(ApplicationUser user)
        {
            int rows = _context.Execute($@"UPDATE AspNetUsers SET
                    UserName = @UserName,
                    NormalizedUserName = @NormalizedUserName,
                    Email = @Email,
                    NormalizedEmail = @NormalizedEmail,
                    EmailConfirmed = @EmailConfirmed,
                    PasswordHash = @PasswordHash,
                    PhoneNumber = @PhoneNumber,
                    PhoneNumberConfirmed = @PhoneNumberConfirmed,
                    TwoFactorEnabled = @TwoFactorEnabled ,	  
					SecurityStamp  = @SecurityStamp,
					ConcurrencyStamp = @ConcurrencyStamp,
					LockoutEnd = @LockoutEnd,			
					LockoutEnabled = @LockoutEnabled,
					AccessFailedCount = @AccessFailedCount
                    WHERE Id = @Id", user);

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not update user {user.Email}." });
        }

        public ApplicationUser FindByEmailAsync(string email)
        {
            string sql = $@"SELECT Id,
                            UserName,
                            NormalizedUserName,
                            Email,
                            NormalizedEmail,
                            EmailConfirmed,
                            PasswordHash,
                            SecurityStamp,
                            ConcurrencyStamp,
                            PhoneNumber,
                            PhoneNumberConfirmed,
                            TwoFactorEnabled,
                            LockoutEnd,
                            LockoutEnabled,
                            AccessFailedCount
						FROM AspNetUsers  
						WHERE NormalizedEmail = @NormalizedEmail;";

            return _context.QuerySingleOrDefault<ApplicationUser>(sql, new
            {
                NormalizedEmail = email
            });
        }
        public ApplicationUser FindByPhoneNumberAsync(string phoneNumber)
        {
            string sql = $@"SELECT Id,
                            UserName,
                            NormalizedUserName,
                            Email,
                            NormalizedEmail,
                            EmailConfirmed,
                            PasswordHash,
                            SecurityStamp,
                            ConcurrencyStamp,
                            PhoneNumber,
                            PhoneNumberConfirmed,
                            TwoFactorEnabled,
                            LockoutEnd,
                            LockoutEnabled,
                            AccessFailedCount
						FROM AspNetUsers  
						WHERE PhoneNumber = @phoneNumber;";

            return _context.QuerySingleOrDefault<ApplicationUser>(sql, new
            {
                PhoneNumber = phoneNumber
            });
        }
        public IdentityResult AddToRoleAsync(ApplicationUser user, string roleName)
        {

            string sql = $@"SELECT Id from AspNetRoles Where name = @RoleName;
                        ";

            var roleId = _context.QuerySingleOrDefault<string>(sql, new
            {
                RoleName = roleName
            });
            sql = $@"INSERT INTO AspNetUserRoles
                        (UserId,
                        RoleId)
                        VALUES
                        (@UserId,
                        @RoleId);
                        ";
            var rows = _context.Execute(sql, new
            {
                UserId = user.Id,
                RoleId = roleId
            });


            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user role {user.Email}." });

        }

        public IList<string> GetRolesAsync(ApplicationUser user)
        {
            string sql = $@"SELECT AspNetRoles.Name from AspNetUserRoles  inner join AspNetRoles on AspNetUserRoles.RoleId = AspNetRoles.Id Where UserId = @UserId;";
            var result = _context.Query<string>(sql, new
            {
                UserId = user.Id

            });

            var roleList = result.Select(x => Convert.ToString(x)).ToList();
            return roleList;


        }

        public bool IsInRoleAsync(ApplicationUser user, string roleName)
        {
            string sql = $@"SELECT AspNetRoles.Name from AspNetUserRoles  inner join AspNetRoles on AspNetUserRoles.RoleId = AspNetRoles.Id
                        Where UserId = @UserId and AspNetRoles.Name = @RoleName;";
            var result = _context.QuerySingleOrDefault<string>(sql, new
            {
                UserId = user.Id,
                RoleName = roleName
            });

            if (!string.IsNullOrEmpty(result))
                return true;
            else
                return false;
        }

        public bool BlukDisableLockout()
        {
            string sql = $@"Update AspNetUsers set LockoutEnabled = 0";
            var result = _context.Execute(sql, null);
            return result > 0;
        }
       
    }
}
