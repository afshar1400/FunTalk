using FluentValidation;
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
    //request 
    public class LoginUser:IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    //validation
    public class LoginUserValidation : AbstractValidator<LoginUser> 
    {
        public LoginUserValidation()
        {
            RuleFor(ur => ur.Email).NotEmpty().NotNull().WithMessage("email should not be empty");
            RuleFor(ur => ur.Password).NotEmpty().NotNull().WithMessage("password should not be empty");
        }
    }

    //handler
    public class LoginUserHandler : IRequestHandler<LoginUser, string>
    {
        private readonly IIdentityService _identity;

        public LoginUserHandler(IIdentityService identity)
        {
            _identity = identity;
        }
        public async Task<string> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var token = await _identity.login(request.Email, request.Password);
            return token;
        }
    }
}
