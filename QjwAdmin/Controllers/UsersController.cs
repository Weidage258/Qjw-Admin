using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.User;
using Model.Other;
using QjwAdmin.Config;

namespace QjwAdmin.Controllers
{
   
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _users;
        public UsersController(IUserService Users)
        {
            _users = Users;
        }
        [HttpPost]
        public ApiResult GetUsers(UserReq req)
        {
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_users.GetUsers(req));
        }
        [HttpGet]
        public ApiResult GetUsersById(long id)
        {
            return ResultHelper.Success(_users.GetUsersById(id));
        }
        [HttpPost]
        public ApiResult Add(UserAdd req)
        {
            //获取当前登录人信息 
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_users.Add(req, userId));
        }
        [HttpPost]
        public ApiResult Edit(UserEdit req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_users.Edit(req, userId));
        }
        [HttpGet]
        public ApiResult Test(string a) {

            return ResultHelper.Success(_users.Test(a));
        }
        [HttpGet]
        public ApiResult Del(long id)
        {
            return ResultHelper.Success(_users.Del(id));
        }
        [HttpGet]
        public ApiResult BatchDel(string ids)
        {
            return ResultHelper.Success(_users.BatchDel(ids));
        }
        [HttpGet]
        public ApiResult SettingRole(string pid, string rids)
        {
            return ResultHelper.Success(_users.SettingRole(pid, rids));
        }
        [HttpGet]
        public ApiResult EditNickNameOrPassword(string nickName, string? password)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_users.EditNickNameOrPassword(userId, nickName, password));
        }
    }
}
