using Dapper;
using MediatR;
using Commodum.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commodum.Application.CQRS.Network.Queries
{
    public class ValidateFormQuery : IRequest<string>
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public class ValidateFormQueryHandler : IRequestHandler<ValidateFormQuery, string>
        {

            private readonly IDBContext _dBContext;
            public ValidateFormQueryHandler(IDBContext dBContext)
            {
                _dBContext = dBContext;
            }

            public async Task<string> Handle(ValidateFormQuery request, CancellationToken cancellationToken)
            {
                var parameter = new DynamicParameters();

                parameter.Add("@Key", request.Key);
                parameter.Add("@Value", request.Value);

                var response = _dBContext.QuerySingleOrDefault<string>("Sp_Validate_Fields", parameter, System.Data.CommandType.StoredProcedure);
                if (response != null)
                {
                    return await Task.FromResult(response);

                }
                return await Task.FromResult(response);
            }
        }
    }
}
