using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commodum.Application.Infrastructure.Response
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool IsError { get; set; }
        public string StatusCode { get; set; }
        public string Description { get; set; }
        public string SuccessMessage { get; set; }
        public override string ToString()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            return JsonConvert.SerializeObject(this, settings);
        }
    }
}
