using FluentValidation;
using FunTalk.Application.Common.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunTalk.Application.Users.Commands
{
    //request 
    public class CreateUser:IRequest<bool>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool Private { get; set; }
    }

    //validation
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(usr => usr.Email).NotNull().NotEmpty().WithMessage("email should not be empty");
            RuleFor(usr => usr.Username).NotNull().NotEmpty().WithMessage("username should not be empty");
            RuleFor(usr => usr.Password).NotNull().NotEmpty().WithMessage("password should not be empty");
        }
    }


    public class CreateUserHandler : IRequestHandler<CreateUser, bool>
    {
        private readonly IIdentityService identity;

        public CreateUserHandler(IIdentityService identity)
        {
            this.identity = identity;
        }
        public async Task<bool> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var usr =await identity.RegisterUser(request.Email, request.Username, request.Password,request.Private);
            return usr;
        }
    }

}
