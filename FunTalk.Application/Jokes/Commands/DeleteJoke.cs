using FluentValidation;
using FunTalk.Application.Common.Interface;
using FunTalk.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunTalk.Application.Jokes.Commands
{
    public record DeleteJoke(long id) : IRequest<Joke> { }

     //validation
    public class DeleteJokeValidator : AbstractValidator<DeleteJoke>
    {
        public DeleteJokeValidator()
        {
            RuleFor(el => el.id).NotNull().NotEmpty().WithMessage("id should not be empty");
        }
    }

    public class DeleteJokeHandler : IRequestHandler<DeleteJoke, Joke>
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserId userId;

        public DeleteJokeHandler(IApplicationDbContext context,ICurrentUserId userId)
        {
            this.context = context;
            this.userId = userId;
        }
        public async Task<Joke> Handle(DeleteJoke req, CancellationToken cancellationToken)
        {
            var joke = context.Jokes.FirstOrDefault(x => x.Id == req.id && x.UserId == userId.UserId);
            if (joke == null)
                return null;
            context.Jokes.Remove(joke);
            await context.SaveChangesAsync(cancellationToken);
            return joke;
        }
    }
}
