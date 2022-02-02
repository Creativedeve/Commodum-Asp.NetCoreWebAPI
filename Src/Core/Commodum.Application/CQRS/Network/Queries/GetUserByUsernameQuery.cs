using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Commodum.Domain.Entities.Network;
using Commodum.Application.Interfaces;

namespace Commodum.Application.CQRS.Network.Queries
{
    public class GetUserByUsernameQuery : IRequest<UserModel>
    {

        public string UserName { get; set; }
        public int Option { get; set; }

        public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserModel>
        {
            private readonly IDBContext _dBContext;


            public GetUserByUsernameQueryHandler(IDBContext dBContext)
            {
                _dBContext = dBContext;
            }
            public async Task<UserModel> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
            {
                var parameter = new DynamicParameters();

                parameter.Add("@UserName", request.UserName);
                parameter.Add("@Option", request.Option);

                var response = _dBContext.QuerySingleOrDefault<UserModel>("Sp_Get_User_By_UserName", parameter, System.Data.CommandType.StoredProcedure);
                if (response != null)
                {
                    return await Task.FromResult(response);

                }
                return await Task.FromResult(response);

            }
        }
    }
}

