using Dapper;
using MediatR;
using Commodum.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commodum.Application.CQRS.Network.Queries
{
    public class SearchUserByUsernameQuery : IRequest<IEnumerable<string>>
    {
        public string UserName { get; set; }
        public int Option { get; set; }
        public class SearchUserByUsernameQueryHandler : IRequestHandler<SearchUserByUsernameQuery, IEnumerable<string>>
        {
            private readonly IDBContext _dBContext;
            public SearchUserByUsernameQueryHandler(IDBContext dBContext)
            {
                _dBContext = dBContext;
            }
            public async Task<IEnumerable<string>> Handle(SearchUserByUsernameQuery request, CancellationToken cancellationToken)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UserName", request.UserName);
                parameter.Add("@Option", request.Option);
                var response = _dBContext.Query<string>("Sp_Search_User_By_Username", parameter, System.Data.CommandType.StoredProcedure);
                if (response.Count() > 0)
                {
                    return await Task.FromResult(response);
                }

                return await Task.FromResult(response);
            }
        }
    }
}
