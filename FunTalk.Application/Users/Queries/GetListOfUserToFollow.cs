using FunTalk.Application.Common.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunTalk.Application.Users.Queries
{
    public class GetListOfUserToFollow:IRequest<List<ListOfUserToFollow>>
    {
        
    }

    public class GetListOfUserToFollowHandler : IRequestHandler<GetListOfUserToFollow, List<ListOfUserToFollow>>
    {
        private readonly ICurrentUserId userId;
        private readonly IIdentityService identityService;

        public GetListOfUserToFollowHandler(ICurrentUserId userId, IIdentityService identityService)
        {
            this.userId = userId;
            this.identityService = identityService;
        }
        public Task<List<ListOfUserToFollow>> Handle(GetListOfUserToFollow request, CancellationToken cancellationToken)
        {
            return identityService.GetListOfUserToFollow(userId.UserId);
        }
    }

    public class ListOfUserToFollow
    {
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
