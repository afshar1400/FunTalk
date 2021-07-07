using AutoMapper;
using FunTalk.Application.Common.Helper;
using FunTalk.Application.Common.Interface;
using FunTalk.Application.Jokes.ViewModels;
using FunTalk.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunTalk.Application.Jokes.Queries
{
    public class GetJokesForExplorer:IRequest<JokesForExplorer>
    {
        public int pageSize { get; set; } = 25;
        public int pageIndex { get; set; } = 1;

    }

    public class GetJokesForExplorereHandler : IRequestHandler<GetJokesForExplorer, JokesForExplorer>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetJokesForExplorereHandler(IApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<JokesForExplorer> Handle(GetJokesForExplorer request, CancellationToken cancellationToken)
        {
            var jks = from joke in context.Jokes
                      let Likenums = context.Likes.Where(x => x.JokeId == joke.Id).Count()
                      let cmtNums = context.MainCmts.Where(x => x.JokeId == joke.Id).Count()
                      orderby joke.Created descending
                      select new Joke { Id = joke.Id, LikeCount = Likenums, IsPrivate = joke.IsPrivate, Text = joke.Text, Created = joke.Created ,CmtCount=cmtNums};
            //todo use linq and join
            var jokesPaginated=await PaginationList<Joke>.CreateAsync(jks, request.pageIndex, request.pageSize);
            var Jokes = mapper.Map<JokesForExplorer>(jokesPaginated);
            return Jokes;
        }
    }

    public class JokesForExplorer
    {
        public IEnumerable<JokeVm> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
    }
}
