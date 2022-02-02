using Microsoft.AspNetCore.Identity;
using Commodum.Application.Interfaces;
using Commodum.Domain.Entities.Identity;

namespace Commodum.Persistence.Identity.DapperTables
{
    public class DapperRoleTable
    {
        private readonly IDBContext _context;
        public DapperRoleTable(IDBContext context)
        {
            _context = context;
        }
        public IdentityResult CreateAsync(ApplicationRole role)
        {
            string sql = $@"INSERT INTO AspNetRoles
                            (Name,
                            NormalizedName,
                            ConcurrencyStamp
                            )
                            VALUES
                            (@Name, @NormalizedName, @ConcurrencyStamp);

                                                ";

            int rows = _context.Execute(sql, role);

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not update role {role.Name}." });
        }
        public IdentityResult UpdateAsync(ApplicationRole role)
        {

            int rows = _context.Execute($@"UPDATE AspNetRoles SET
                    Name = @Name,
                    NormalizedName=@NormalizedName,
                    ConcurrencyStamp=@ConcurrencyStamp
                    WHERE Id = @Id", role);

            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not update user {role.Name}." });

        }
        public ApplicationRole FindByIdAsync(string roleId)
        {
            string sql = $@"SELECT Id ,Name 
						FROM AspNetRoles 
						WHERE Id = @Id;";

            return _context.QuerySingleOrDefault<ApplicationRole>(sql, new
            {
                Id = roleId
            });
        }
        public ApplicationRole FindByNameAsync(string name)
        {
            string sql = $@"SELECT Id ,Name , NormalizedName, ConcurrencyStamp
						FROM AspNetRoles 
						WHERE Name = @Name;";

            return _context.QuerySingleOrDefault<ApplicationRole>(sql, new
            {
                Name = name
            });
        }
    }
}
