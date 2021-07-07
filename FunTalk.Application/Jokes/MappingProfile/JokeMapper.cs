using AutoMapper;
using FunTalk.Application.Common.Helper;
using FunTalk.Application.Jokes.Commands;
using FunTalk.Application.Jokes.Queries;
using FunTalk.Application.Jokes.ViewModels;
using FunTalk.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunTalk.Application.Jokes.MappingProfile
{
    public class JokeMapper : Profile
    {
        public JokeMapper()
        {
            CreateMap<Joke, CreateJoke>();
            CreateMap<CreateJoke, Joke>();

            CreateMap<Joke, JokeVm>();
            CreateMap<JokeVm, Joke>();

            CreateMap<PaginationList<Joke>, JokesForExplorer>().ConstructUsing(x =>new JokesForExplorer {Items=x.Items.Select(n=>new JokeVm {Id=n.Id,Created=n.Created,IsPrivate=n.IsPrivate,LikeCount=n.LikeCount,Text=n.Text,UserId=n.UserId }),PageIndex=x.PageIndex,PageSize=x.PageSize,TotalCount=x.TotalCount,TotalPage=x.TotalPage });
            
            CreateMap<PaginationList<Joke>, JokesForTimeline>().ConstructUsing(x =>new JokesForTimeline {Items=x.Items.Select(n=>new JokeVm {Id=n.Id,Created=n.Created,IsPrivate=n.IsPrivate,LikeCount=n.LikeCount,Text=n.Text,UserId=n.UserId }),PageIndex=x.PageIndex,PageSize=x.PageSize,TotalCount=x.TotalCount,TotalPage=x.TotalPage });
            
        }
    }
}
