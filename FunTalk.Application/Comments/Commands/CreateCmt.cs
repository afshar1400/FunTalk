using AutoMapper;
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

namespace FunTalk.Application.Comments.Commands
{

    //number of cmts for a joke
    public class CreateCmt : IRequest<CreateCmtResponse>
    {
        public long JokeId { get; set; }
        public long CmtId { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
    }
    //validation
    public class CreateCmtValidator : AbstractValidator<CreateCmt>
    {
        public CreateCmtValidator()
        {
            RuleFor(x => x.JokeId).NotEmpty().NotNull().WithMessage("joke id must not be empty");
            RuleFor(x => x.Text).NotEmpty().NotNull().WithMessage("cmt text must not be empty");
        }
    }
    //handler
    public class CreateCmtHandler : IRequestHandler<CreateCmt, CreateCmtResponse>
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserId userId;
        private readonly IMapper mapper;

        public CreateCmtHandler(
            IApplicationDbContext context
            , ICurrentUserId userId,
            IMapper mapper)
        {
            this.context = context;
            this.userId = userId;
            this.mapper = mapper;
        }
        public async Task<CreateCmtResponse> Handle(CreateCmt req, CancellationToken cancellationToken)
        {
            req.UserId = userId.UserId;
            dynamic cmt;
            //for checking if has cmtID or not ==> if not it is a main comment
            if (req.CmtId != 0 && req.CmtId > 0)
            {
                cmt = mapper.Map<SubCmt>(req);
                await context.SubCmts.AddAsync(cmt);
            }
            else
            {
                cmt = mapper.Map<MainCmt>(req);
                await context.MainCmts.AddAsync(cmt);
            }
            await context.SaveChangesAsync(cancellationToken);
            var cmtCount = context.MainCmts.AsNoTracking().Where(x => x.JokeId == req.JokeId).Count();
            return new CreateCmtResponse { CmtCount = cmtCount, CmtId = cmt.Id, CmtText = req.Text, JokeId = req.JokeId };
        }
    }

    //response
    public class CreateCmtResponse
    {
        public long JokeId { get; set; }
        public int CmtCount { get; set; }
        public string CmtText { get; set; }
        public long CmtId { get; set; }
    }
}
