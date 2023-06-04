using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.User;
using Model.Other;
using QjwAdmin.Config;

namespace QjwAdmin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    { 
        private IUserService _userService;
        private ICustomJWTService _jwtService;
        public LoginController(IUserService userService, ICustomJWTService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        [HttpGet]
        public async Task<ApiResult> GetToken(string name, string password)
        {
            var res = Task.Run(() =>
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
                {
                    return ResultHelper.Error("参数不能为空");
                }
                UserRes user = _userService.GetUser(name, password);
                if (string.IsNullOrEmpty(user.Name))
                {
                    return ResultHelper.Error("账号不存在，用户名或密码错误！");
                }
                return ResultHelper.Success(_jwtService.GetToken(user));
            });
            return await res;
        }
    }
}
