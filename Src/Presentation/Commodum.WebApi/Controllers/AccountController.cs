using Commodum.Application.CQRS.Account;
using Commodum.Application.CQRS.Account.Commands;
using Commodum.Application.CQRS.Account.Queries;
using Commodum.Application.Infrastructure.Response;
using Commodum.Domain.Entities.Account;
using Commodum.Domain.Entities.Network;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commodum.WebApi.Controllers
{
    [ApiController]
    public class AccountController : BaseController
    {
        [HttpGet]
        [Route("api/get-roles")]
        public ApiResponse<IEnumerable<RolesDto>> GetRoles()
        {
            var result = Mediator.Send(new GetRolesQuery()).Result;
            return result;
        }

        [HttpGet]
        [Route("api/get-all-users")]
        public ApiResponse<IEnumerable<GetAllUsersModel>> GetAllUsers()
        {
            var result = Mediator.Send(new GetAllUsersQuery()).Result;
            return result;
        }

        [HttpPost]
        [Route("api/create-user")]
        public async Task<ApiResponse<bool>> CreateUser([FromBody] CreateUserCommand createUserCommand)
        {
            createUserCommand.CurrentUser = User.Identity.Name;
            var result = await Mediator.Send(createUserCommand);
            return result;
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<ApiResponse<Login>> Login(LoginQuery loginQuery)
        {
            var result = await Mediator.Send(loginQuery);
            return result;
        }

        [HttpGet]
        [Route("api/logout")]
        public async Task<ApiResponse<bool>> Logout()
        {
            var result = await Mediator.Send(new LogoutQuery() { JwtToken = JwtToken });
            return result;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/get-forget-password-token")]
        public async Task<ApiResponse<UserPasswordLink>> GetForgetPasswordToken(GetForgetPasswordTokenCommand getForgetPasswordTokenCommand)
        {
            var result = await Mediator.Send(getForgetPasswordTokenCommand);
            return result;
        }

        [HttpGet]
        [Route("api/get-user-information")]
        public async Task<ApiResponse<UserTree>> GetUserInformation()
        {
            var result = Mediator.Send(new GetUserNameQuery { Username = User.Identity.Name }).Result;
            return result;
        }
    }
}