using Commodum.Application.Interfaces;
using Dapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Commodum.Application.CQRS.Network.Queries
{
    public class CalculateSponsorChildrenQuery : IRequest<int>
    {
        public string SponsorUsername { get; set; }

        public class CalculateSponsorChildrenQueryHandler : IRequestHandler<CalculateSponsorChildrenQuery, int>
        {
            private readonly IDBContext _dBContext;
            public CalculateSponsorChildrenQueryHandler(IDBContext dBContext)
            {
                _dBContext = dBContext;
            }

            public async Task<int> Handle(CalculateSponsorChildrenQuery request, CancellationToken cancellationToken)
            {
                var parameter = new DynamicParameters();

                parameter.Add("@SponsorUsername", request.SponsorUsername);
              
                var response = _dBContext.QuerySingleOrDefault<int>("Sp_Calculate_Sponsor_Children", parameter, System.Data.CommandType.StoredProcedure);
                if (response > 0)
                {
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(response);
            }
        }
    }
}
