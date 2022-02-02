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
    public class GetSponsorUserPositionQuery : IRequest<string>
    {
        public string SponsorUsername { get; set; }

        public class GetSponsorUserPositionQueryHandler : IRequestHandler<GetSponsorUserPositionQuery, string>
        {
            private readonly IDBContext _dBContext;
            public GetSponsorUserPositionQueryHandler(IDBContext dBContext)
            {
                _dBContext = dBContext;
            }

            public async Task<string> Handle(GetSponsorUserPositionQuery request, CancellationToken cancellationToken)
            {
                var parameter = new DynamicParameters();

                parameter.Add("@SponsorUsername", request.SponsorUsername);
              
                var response = _dBContext.QuerySingleOrDefault<string>("Sp_Calculate_Sponsor_Position", parameter, System.Data.CommandType.StoredProcedure);
                if (response != null)
                {
                    return await Task.FromResult(response);

                }
                return await Task.FromResult(response);
            }
        }
    }
}
