using FunTalk.Application.Likes.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunTalk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LikeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost,Route("{id}")]
        public async Task<IActionResult> CreateLike(long id)
        {
            var res = await _mediator.Send(new CreateLike { JokeId=id});
            return Ok(res);
        }
    }
}
