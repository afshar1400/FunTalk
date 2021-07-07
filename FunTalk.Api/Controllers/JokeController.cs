using FunTalk.Application.Jokes.Commands;
using FunTalk.Application.Jokes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FunTalk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JokeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public JokeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// create joke
        /// </summary>
        /// <route>/api/joke</route>
        /// <param name="joke"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateJoke([FromBody] CreateJoke joke)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            joke.UserId = userId;
            var res = await _mediator.Send(joke);
            return Ok(res);
        }

        [HttpGet, Route("Explorer")]
        public async Task<IActionResult> GetAllJokesForExplorer([FromQuery] int pageIndex = 1, int pageSize = 25)
        {
            var res = await _mediator.Send(new GetJokesForExplorer { pageIndex = pageIndex, pageSize = pageSize });

            return Ok(res);
        }

        [HttpGet, Route("Timeline")]
        public async Task<IActionResult> GetAllJokesForTimeline([FromQuery] int pageIndex = 1, int pageSize = 25)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await _mediator.Send(new GetJokesForTimeline { pageIndex = pageIndex, pageSize = pageSize, UserId = userId });
            return Ok(res);
        }

        [HttpDelete,Route("{id}")]
        public async Task<IActionResult> DeleteJoke(long id)
        {
            var res = await _mediator.Send(new DeleteJoke(id));
            return Ok(res);
        }


        [HttpGet,Route("{id}")]
        public async Task<IActionResult> GetJokeDetail(long id)
        {
            var res = await _mediator.Send(new GetJokeDetail { JokeId=id});
            return Ok(res);
        }
    }
}
