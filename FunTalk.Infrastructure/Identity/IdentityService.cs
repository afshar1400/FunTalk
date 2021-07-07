using FunTalk.Application.Common.Interface;
using FunTalk.Application.Users.Queries;
using FunTalk.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunTalk.Infrastructure.Identity
{
   

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userMgr;
        private readonly SignInManager<AppUser> _signMgr;
        private readonly IJwtManager _jwtMgr;
        private readonly IApplicationDbContext context;

        public IdentityService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signMgr,
            IJwtManager jwtMgr,
            IApplicationDbContext context)
        {
            _userMgr = userManager;
            _signMgr = signMgr;
            _jwtMgr = jwtMgr;
            this.context = context;
        }

        public async Task<bool> UserIsPrivate(string usrId)
        {
            var usr = await _userMgr.Users.FirstAsync(x => x.Id == usrId);
            return usr.IsPrivate;
        }

        public async Task<bool> RegisterUser(string email,string Username, string password,bool Private)
        {
            var user = new AppUser { UserName = Username ,Email=email,IsPrivate=Private};
            var usr = await _userMgr.CreateAsync(user, password);
            return usr.Succeeded;
        }

        public async Task<string> login(string email, string pass)
        {
            var finduser = await _userMgr.Users.FirstAsync(x => x.Email == email);
            if (finduser == null)
                return null;
            var result = await _signMgr.PasswordSignInAsync(finduser, pass, false, false);

            if (result.Succeeded)
            {
                var token= await _jwtMgr.GenerateToken(finduser.Id);
                return token;
            }
            return null;
        }

        public async Task<string> GetUserIdByUserName(string username)
        {
            var usr = await _userMgr.Users.FirstAsync( x => x.UserName == username);
            return usr.Id;
        }

        public async Task<(string username,string token)> GetUserInfo(string userId)
        {
            var user =await  _userMgr.FindByIdAsync(userId);
            var username = user.UserName;

           var  token =await _jwtMgr.GenerateToken(userId);
            return (username, token);
           
        }

        public async Task<List<ListOfUserToFollow>> GetListOfUserToFollow(string userId)
        {
            var list = new List<ListOfUserToFollow>();
            var followedUser = await context.Follows.AsNoTracking().Where(x => x.UserId == userId).Select(x => x.peopleId).ToListAsync();
            followedUser.Add(userId);
            var user =await _userMgr.Users.Where(x => !followedUser.Contains(x.Id)).OrderBy(x =>x.Id).ToListAsync();

            foreach (var item in user)
            {
                list.Add(new ListOfUserToFollow {UserId=item.Id,Username=item.UserName });
            }

            return list;
        }
    }
}
