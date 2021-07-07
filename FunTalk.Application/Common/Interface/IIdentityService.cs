using FunTalk.Application.Users.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunTalk.Application.Common.Interface
{
    public interface IIdentityService
    {
        Task<bool> UserIsPrivate(string usrId);
        Task<string> login(string email, string pass);
        Task<bool> RegisterUser(string email,string Username, string password,bool Private);

        Task<string> GetUserIdByUserName(string username);
        Task<(string username,string token)> GetUserInfo(string userId);
        Task<List<ListOfUserToFollow>> GetListOfUserToFollow(string userId);
    }
}
