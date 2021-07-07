using FunTalk.Application.Common.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FunTalk.Api.Services
{
   
    public class CurrentUserId : ICurrentUserId
    {
        private readonly IHttpContextAccessor httpContext;

        public CurrentUserId(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }
        public string UserId => httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
