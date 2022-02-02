using Commodum.Application.CQRS.Account.Commands;
using Commodum.Application.CQRS.Network.Commands;
using Commodum.Application.CQRS.Network.Queries;
using Commodum.Application.Infrastructure.Response;
using Commodum.Domain.Entities.Network;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commodum.WebApi.Controllers
{
    [ApiController]
    public class NetwrokController : BaseController
    {
        [HttpPost]
        [Route("api/create-member")]
        [Authorize]
        public async Task<ApiResponse<bool>> CreateMember([FromBody] CreateMemberCommand createMemberCommand)
        {
            var createUserCommand = new CreateUserCommand()
            {
                FirstName = createMemberCommand.FirstName,
                LastName = createMemberCommand.LastName,
                UserName = createMemberCommand.UserName,
                Email = createMemberCommand.Email,
                Password = createMemberCommand.Password,
                UserType = createMemberCommand.UserType
            };

            var resultUserCreated = Mediator.Send(createUserCommand).Result;
            if (resultUserCreated.IsError)
                return new ApiResponse<bool>() { IsError = true, Data = false, Description = resultUserCreated.Description };

            var CalculateSponsorChildren = Mediator.Send(new CalculateSponsorChildrenQuery()
            {
                SponsorUsername = createMemberCommand.SponsorUserName
            }).Result;

            var CalculateUserExpiry = Mediator.Send(new CalculateUserExpiryQuery()
            {
                Username = createMemberCommand.UserName

            }).Result;

            //if (CalculateUserExpiry != null)
            //{
            if ((createMemberCommand.SponsorUserName != "company") && (createMemberCommand.SponsorUserName != "svend") && (createMemberCommand.SponsorUserName != "worldview") && (createMemberCommand.SponsorUserName != "master") && (createMemberCommand.SponsorUserName != "mirroradmin"))
            {
                if (CalculateSponsorChildren == 0)
                {
                    var GetSponsorUserPosition = Mediator.Send(new GetSponsorUserPositionQuery()
                    {
                        SponsorUsername = createMemberCommand.SponsorUserName
                    }).Result;

                    createMemberCommand.Position = GetSponsorUserPosition;
                }
            }
            
            if (CalculateUserExpiry == null)
            {
                createMemberCommand.IsActive = false;
            }
            else {
                createMemberCommand.IsActive = true;
            }
            createMemberCommand.CurrentUser = User.Identity.Name;
            var result = await Mediator.Send(createMemberCommand);
            return result;
            //}
            //else {
            //    return new ApiResponse<bool>() { IsError = false, Data = false, Description = resultUserCreated.Description };
            //}

        }

        [HttpGet]
        [Route("api/fetch-tree")]
        [Authorize]
        public IEnumerable<UserTree> FetchTree()
        {
            var result = Mediator.Send(new GetUserTreeQuery { UserName = User.Identity.Name }).Result;
            return result;
        }

        [HttpGet]
        [Route("api/validate-form-fields/{inputField}/{value}")]
        public Task<string> ValidateForm([FromRoute] string inputField, string value)
        {
            var result = Mediator.Send(new ValidateFormQuery { Key = inputField, Value = value });
            return result;
        }

        [HttpGet]
        [Route("api/search-user-by-username/{username}/{option}")]
        public async Task<IEnumerable<string>> SearchUserByUsername(string username, int option)
        {
            var result = await Mediator.Send(new SearchUserByUsernameQuery { UserName = username, Option = option });
            return result;
        }

        [HttpGet]
        [Route("api/get-user-by-username/{username}/{option}")]
        public async Task<UserModel> GetUserByUsername(string username, int option)
        {
            var result = await Mediator.Send(new GetUserByUsernameQuery { UserName = username, Option = option });
            return result;
        }

        [HttpGet]
        [Route("api/get-username-by-sponsorid")]
        public UserTree GetUserNameBySponsorId([FromQuery] string sponsorid)
        {
            var result = Mediator.Send(new GetUserNameBySponsorIdQuery { SponsorId = sponsorid }).Result;
            return result;
        }
    }
}