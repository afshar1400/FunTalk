using FunTalk.Application.Users.Commands;
using FunTalk.Application.Users.Queries;
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
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("register"),HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Register(CreateUser createUser)
        {
            var res = await _mediator.Send(createUser);
            return Ok(res);
        } 
        [Route("login"),HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Login(LoginUser login)
        {
            var res = await _mediator.Send(login);
            return Ok(res);
        }


        [Authorize]
        [HttpGet,Route("userInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await _mediator.Send(new GetUser { userId = userId });
            return Ok(res);
        }


        [Authorize]
        [HttpGet,Route("follow")]
        public async Task<IActionResult> GetListOfUserToFollow()
        {
            var res = await _mediator.Send(new GetListOfUserToFollow());
            return Ok(res);
        } 
        [Authorize]
        [HttpPost,Route("follow")]
        public async Task<IActionResult> FollowUserById(FollowUserById people)
        {
            var res = await _mediator.Send(people);
            return Ok(res);

        }
    }
}
