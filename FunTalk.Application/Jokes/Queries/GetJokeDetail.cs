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
using Microsoft.EntityFrameworkCore;

namespace FunTalk.Application.Jokes.Queries
{
    public class GetJokeDetail : IRequest<JokeDetailResponse>
    {
        public long JokeId { get; set; }
    }


    public class GetJokeDetailValidator : AbstractValidator<GetJokeDetail>
    {
        public GetJokeDetailValidator()
        {
            RuleFor(x => x.JokeId).NotEmpty().NotNull().WithMessage("joke id should not be empty");
        }
    }

    public class GetJokeDetailHandler : IRequestHandler<GetJokeDetail, JokeDetailResponse>
    {
        private readonly IApplicationDbContext context;

        public GetJokeDetailHandler(IApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<JokeDetailResponse> Handle(GetJokeDetail request, CancellationToken cancellationToken)
        {
            //var jokeDetail = from jok in context.Jokes
            //            where jok.Id == request.JokeId
            //            let comts = context.MainCmts.Where(x => x.JokeId == request.JokeId).Select(x => new Comts { Comment = x.Comment, UserId = x.UserId, Id = x.Id }).ToList()
            //            select new JokeDetailResponse
            //            {
            //                Id = jok.Id,
            //                IsPrivate = jok.IsPrivate,
            //                Created = jok.Created,
            //                Text = jok.Text,
            //                UserId = jok.UserId,
            //                MainCmts = comts,
            //                CmtCount = comts.Count(),

            //         };
            //return await jokeDetail.AsNoTracking().SingleOrDefaultAsync();

            var jk = from joke in context.Jokes
                     where joke.Id == request.JokeId
                     select new JokeDetailResponse
                     {
                         Id = joke.Id,
                         IsPrivate = joke.IsPrivate,
                         Created = joke.Created,
                         Text = joke.Text,
                         UserId = joke.UserId
                     };

            var cmts = await context.MainCmts.Where(x => x.JokeId == request.JokeId).ToListAsync();
            var lks = await context.Likes.Where(x => x.JokeId == request.JokeId).ToListAsync();
            var Detail = await jk.AsNoTracking().FirstOrDefaultAsync();

            Detail.Likes = lks;
            Detail.LikeCount = lks.Count();
            Detail.MainCmts = cmts;
            Detail.CmtCount = cmts.Count();

            return Detail;



        }
    }

    public class JokeDetailResponse {

      public long Id { get; set; }
      public string Text { get; set; }
      public string UserId { get; set; }
      public int LikeCount {get;set;}
      public int CmtCount { get;set; }
      public bool IsPrivate { get; set; }
      public DateTime Created { get; set; }

      public IEnumerable<MainCmt> MainCmts { get; set; }
      public  IEnumerable<Like> Likes { get; set; }
    }



}
