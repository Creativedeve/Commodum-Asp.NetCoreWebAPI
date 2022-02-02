using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Commodum.WebApi.Controllers
{
    public abstract class BaseController : Controller
    {
        private IMediator _mediator;
        public string JwtToken { get; set; }
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);

            if (ctx.HttpContext.Request.Headers.ContainsKey("Authorization") && ctx.HttpContext.Request.Headers["Authorization"].FirstOrDefault().Contains("Bearer"))
            {
                try
                {
                    JwtToken = ctx.HttpContext.Request.Headers["Authorization"].FirstOrDefault().Split("Bearer")[1].Trim();
                }
                catch (Exception)
                {
                    JwtToken = string.Empty;
                }
            }
        }
    }
}