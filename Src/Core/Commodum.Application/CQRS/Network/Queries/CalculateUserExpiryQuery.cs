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

namespace Commodum.Application.CQRS.Network.Queries
{
    public class CalculateUserExpiryQuery : IRequest<string>
    {
        public string Username { get; set; }

        public class CalculateUserExpiryQueryHandler : IRequestHandler<CalculateUserExpiryQuery, string>
        {
            private readonly IDBContext _dBContext;
            public CalculateUserExpiryQueryHandler(IDBContext dBContext)
            {
                _dBContext = dBContext;
            }

            public async Task<string> Handle(CalculateUserExpiryQuery request, CancellationToken cancellationToken)
            {
                var parameter = new DynamicParameters();

                parameter.Add("@Username", request.Username);
              
                var response = _dBContext.QuerySingleOrDefault<string>("Sp_Calculate_User_Expiry", parameter, System.Data.CommandType.StoredProcedure);
                if (response != null)
                {
                    return await Task.FromResult(response);

                }
                return await Task.FromResult(response);
            }
        }
    }
}
