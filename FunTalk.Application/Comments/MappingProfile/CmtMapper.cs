using AutoMapper;
using FunTalk.Application.Comments.Commands;
using FunTalk.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunTalk.Application.Comments.MappingProfile
{
    public class CmtMapper:Profile
    {
        public CmtMapper()
        {
            CreateMap<CreateCmt, SubCmt>().ConstructUsing(x => new SubCmt { JokeId = x.JokeId, UserId = x.UserId, Comment = x.Text, MainCmt = x.CmtId });

            CreateMap<CreateCmt, MainCmt>().ConstructUsing(x => new MainCmt { JokeId = x.JokeId, Comment = x.Text, UserId = x.UserId });
        }
    }
}
