using Commodum.Application.Interfaces;
using Commodum.Domain.Entities.Network;
using Dapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Commodum.Application.CQRS.Network.Queries
{
    public class GetUserNameBySponsorIdQuery : IRequest<UserTree>
    {
      
        public string SponsorId { get; set; }

        public class GetUserNameBySponsorIdHandler : IRequestHandler<GetUserNameBySponsorIdQuery, UserTree>
        {
            private readonly IDBContext _dBContext;


            public GetUserNameBySponsorIdHandler(IDBContext dBContext)
            {
                _dBContext = dBContext;
            }
            public async Task<UserTree> Handle(GetUserNameBySponsorIdQuery request, CancellationToken cancellationToken)
            {
                var parameter = new DynamicParameters();
            
                parameter.Add("@SponsorId", request.SponsorId);
             
                var response = _dBContext.QuerySingleOrDefault<UserTree>("Sp_Get_Username_By_SponsorId", parameter, System.Data.CommandType.StoredProcedure);
                if (response != null)
                {
                    return await Task.FromResult(response);
                   
                }
                return await Task.FromResult(response);

            }
        }
    }
}

   