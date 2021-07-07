using AutoMapper;
using FluentValidation;
using FunTalk.Application.Common.Interface;
using FunTalk.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunTalk.Application.Jokes.Commands
{
    public class CreateJoke:IRequest<Joke>
    {
        [Required]
        [StringLength(500)]
        public string Text { get; set; }

        public string UserId { get; set; }

        public bool Private { get; set; }
    }

    //validation
    public class CreateJokeValidator : AbstractValidator<CreateJoke>
    {
        public CreateJokeValidator()
        {
            RuleFor(usr => usr.Text).NotNull().NotEmpty().WithMessage("text should not be empty");
            RuleFor(usr => usr.UserId).NotNull().NotEmpty().WithMessage("userId should not be empty");
            
        }
    }


    // handler
    public class CreateJokeHandler : IRequestHandler<CreateJoke,Joke>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public CreateJokeHandler(
             IApplicationDbContext context
            ,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<Joke> Handle(CreateJoke req, CancellationToken cancellationToken)
        {
            var joke = mapper.Map<Joke>(req);
            context.Jokes.Add(joke);
           await context.SaveChangesAsync(cancellationToken);
            return joke;
        }
    }
}
