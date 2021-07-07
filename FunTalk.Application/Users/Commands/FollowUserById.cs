using FluentValidation;
using FunTalk.Application.Common.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunTalk.Application.Users.Commands
{
    public class FollowUserById:IRequest<bool>
    {
        public string PeopleId { get; set; }
    }

    public class FollowUserByIdValidation : AbstractValidator<FollowUserById>
    {
        public FollowUserByIdValidation()
        {
            RuleFor(x => x.PeopleId).NotEmpty().NotNull().WithMessage("people id is requried");
        }
    }


    public class FollowUserByIdHandler : IRequestHandler<FollowUserById, bool>
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserId userId;

        public FollowUserByIdHandler(IApplicationDbContext context,ICurrentUserId userId)
        {
            this.context = context;
            this.userId = userId;
        }
        public async Task<bool> Handle(FollowUserById request, CancellationToken cancellationToken)
        {

            var user = context.Follows.AsNoTracking().Where(x => x.UserId == userId.UserId).Select(y => y.peopleId).ToList();

            
            if (user.Contains(request.PeopleId)) 
                return false;

            context.Follows.Add(new Domain.Entity.Follow { UserId = userId.UserId, peopleId = request.PeopleId });
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}
