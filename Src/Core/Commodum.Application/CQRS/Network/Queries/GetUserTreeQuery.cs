using MediatR;
using Commodum.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Commodum.Domain.Entities.Network;
using Commodum.Application.Infrastructure.Response;
using System.Threading.Tasks;
using Dapper;
using Commodum.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Commodum.Application.CQRS.Network.Queries
{
    public class GetUserTreeQuery : IRequest<IEnumerable<UserTree>>
    {
        public string UserName { get; set; }
        public class GetUserTreeQueryHandler : IRequestHandler<GetUserTreeQuery, IEnumerable<UserTree>>
        {
            private readonly IDBContext _dBContext;
            private readonly UserManager<ApplicationUser> _userManager;

            public GetUserTreeQueryHandler(IDBContext dBContext, UserManager<ApplicationUser> userManager)
            {
                _dBContext = dBContext;
                _userManager = userManager;
            }
            public async Task<IEnumerable<UserTree>> Handle(GetUserTreeQuery request, CancellationToken cancellationToken)
            {
                var user = _userManager.FindByNameAsync(request.UserName).Result;

                var parameter = new DynamicParameters();
                parameter.Add("@SponsorUserId", user.Id);

                var response = _dBContext.Query<UserTree>("Sp_Prepare_User_Tree_New", parameter, System.Data.CommandType.StoredProcedure);

                if (response != null)
                {
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(response);
            }
        }
    }
}
