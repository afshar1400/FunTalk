using FluentValidation;
using FunTalk.Application.Common.Interface;
using FunTalk.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunTalk.Application.Likes.Commands
{
    //return number of joke's like
    public class CreateLike:IRequest<int>
    {
        public long JokeId { get; set; }   
        
    }


    public class CreateLikeValidator : AbstractValidator<CreateLike>
    {
        public CreateLikeValidator()
        {
            RuleFor(x => x.JokeId).NotEmpty().NotNull().WithMessage("joke id must be valid");
        }
    }

    public class LikeJokeHandler : IRequestHandler<CreateLike, int>
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserId currentUser;

        public LikeJokeHandler(IApplicationDbContext context, ICurrentUserId currentUser)
        {
            this.context = context;
            this.currentUser = currentUser;
        }
        public async Task<int> Handle(CreateLike request, CancellationToken cancellationToken)
        {
            var li = new Like { JokeId = request.JokeId, UserId = currentUser.UserId };
            context.Likes.Add(li);
            await context.SaveChangesAsync(cancellationToken);
            var likeCount = context.Likes.AsNoTracking().Where(x => x.JokeId == request.JokeId).Count();
            return likeCount;
        }
    }
}
