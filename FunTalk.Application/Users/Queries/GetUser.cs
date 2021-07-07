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
    public class GetUser:IRequest<UserInfo>
    {
        public string userId { get; set; }
    }

    //handler
    public class GetUserHandler : IRequestHandler<GetUser, UserInfo>
    {
        private readonly IIdentityService _identity;

        public GetUserHandler(IIdentityService identity)
        {
            _identity = identity;
        }
        public async Task<UserInfo> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var tuple = await _identity.GetUserInfo(request.userId);
            return new UserInfo {UserId=request.userId,token=tuple.token,username=tuple.username };
        }
    }

    public class UserInfo
    {
        public string UserId { get; set; }
        public string  token { get; set; }

        public string username { get; set; }
    }


}
