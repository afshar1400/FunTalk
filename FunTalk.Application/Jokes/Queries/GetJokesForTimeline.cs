using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FunTalk.Application.Common.Helper;
using FunTalk.Application.Common.Interface;
using FunTalk.Application.Jokes.ViewModels;
using FunTalk.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunTalk.Application.Jokes.Queries
{
    public class GetJokesForTimeline : IRequest<JokesForTimeline>
    {
        public string UserId { get; set; }
        public int pageSize { get; set; } = 25;
        public int pageIndex { get; set; } = 1;
    }

    public class GetJokesForTimelineHandler : IRequestHandler<GetJokesForTimeline, JokesForTimeline>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper mapper;

        public GetJokesForTimelineHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }
       
        public async Task<JokesForTimeline> Handle(GetJokesForTimeline request, CancellationToken cancellationToken)
        {
            //linq
            var jks = from joke in _context.Jokes
                      let followingPeople = _context.Follows.AsNoTracking().Where(x => x.UserId == request.UserId).Select(x => x.peopleId).ToList()
                      where (joke.UserId == request.UserId || followingPeople.Contains(joke.UserId))
                      let Likenums = _context.Likes.Where(x => x.JokeId == joke.Id).Count()
                      let cmtNums = _context.MainCmts.Where(x => x.JokeId == joke.Id).Count()
                      orderby joke.Created descending
                      select new Joke { Id = joke.Id, LikeCount = Likenums, IsPrivate = joke.IsPrivate, Text = joke.Text, Created = joke.Created ,CmtCount=cmtNums};
                      

           
            //giving query to paginatied list class
            var jokesPagination = await PaginationList<Joke>.CreateAsync(jks, request.pageIndex, request.pageSize);
            var JokesForTimeline = mapper.Map<JokesForTimeline>(jokesPagination);
            return JokesForTimeline;
        }
    }

    public class JokesForTimeline
    {
        public IEnumerable<JokeVm> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
    }
}
