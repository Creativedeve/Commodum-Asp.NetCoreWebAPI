using Commodum.Application.Infrastructure.Extensions;
using Commodum.Application.Infrastructure.Response;
using Commodum.Application.Interfaces;
using Commodum.Domain.Entities.Identity;
using Commodum.Domain.Enums;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commodum.Application.CQRS.Network.Commands
{
    public class CreateMemberCommand : IRequest<ApiResponse<bool>>
    {
        public string SponsorPNo { get; set; }
        public string SponsorUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string Gender { get; set; }
        public int UserType { get; set; }
        public bool IsActive { get; set; }
        public string CurrentUser { get; set; }
        public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, ApiResponse<bool>>
        {
            private readonly IDBContext _dBContext;
            private readonly UserManager<ApplicationUser> _userManager;
            public CreateMemberCommandHandler(IDBContext dBContext, UserManager<ApplicationUser> userManager)
            {
                _dBContext = dBContext;
                _userManager = userManager;
            }

            public async Task<ApiResponse<bool>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
            {
                var user = _userManager.FindByNameAsync(request.CurrentUser).Result;
                var newlyCreatedUser = _userManager.FindByNameAsync(request.UserName).Result;
                var SponsorUser = _userManager.FindByNameAsync(request.SponsorUserName).Result;

                if (newlyCreatedUser == null)
                {
                    return await Task.FromResult(new ApiResponse<bool>()
                    {
                        Data = false,
                        IsError = true,
                        StatusCode = ((int)ErrorMessages.UserNotExist).ToString(),
                        Description = EnumHelper<ErrorMessages>.GetDisplayValue(ErrorMessages.UserNotExist)

                    });
                }

                var parameter = new DynamicParameters();
                parameter.Add("@SponsorUserId", SponsorUser.Id);
                parameter.Add("@SponsorPNo", request.SponsorPNo);
                parameter.Add("@SponsorUsername", request.SponsorUserName);
                parameter.Add("@SponsoredUserId", newlyCreatedUser.Id);
                parameter.Add("@SponsoredPNo", request.UserName + "-" + newlyCreatedUser.Id);
                parameter.Add("@SponsoredUsername", request.UserName);
                parameter.Add("@Position", request.Position);
                parameter.Add("@Gender", request.Gender);
                parameter.Add("@IsActive", request.IsActive);
                parameter.Add("@CreatedBy", user.Id);

                var response = _dBContext.QuerySingleOrDefault<int>("Sp_Create_Member", parameter, CommandType.StoredProcedure);

                if (response <= 0)
                {
                    return await Task.FromResult(new ApiResponse<bool>()
                    {
                        Data = false,
                        IsError = true,
                        StatusCode = ((int)ErrorMessages.UserPlacementFailed).ToString(),
                        Description = EnumHelper<ErrorMessages>.GetDisplayValue(ErrorMessages.UserPlacementFailed)
                    });
                }

                var parameter2 = new DynamicParameters();

                parameter2.Add("@UserId", newlyCreatedUser.Id);
                parameter2.Add("@Username", request.UserName);
                var subscriptionResponse = _dBContext.QuerySingleOrDefault<int>("Sp_Create_Subscription", parameter2, CommandType.StoredProcedure);



                return await Task.FromResult(new ApiResponse<bool>()
                {
                    Data = true,
                    IsError = false,
                    StatusCode = ((int)SuccessMessages.UserPlaced).ToString(),
                    Description = EnumHelper<SuccessMessages>.GetDisplayValue(SuccessMessages.UserPlaced)
                });
            }
        }
    }
}
