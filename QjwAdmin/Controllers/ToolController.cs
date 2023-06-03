using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using SqlSugar;
using System.Reflection;

namespace QjwAdmin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolController:ControllerBase
    {
        //构造函数注入
        public ISqlSugarClient _db;
        public ToolController(ISqlSugarClient db) { 
           _db= db;
        }
        [HttpGet]
        public string InitDateBase()
        {
          
            string res = "OK";
            //如果不存在则创建数据库
            _db.DbMaintenance.CreateDatabase();
            //创建表S
            //string nspace = "Model.Entitys";
            ////通过反射读取我们想要的类
            //Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll").GetTypes().Where(p => p.Namespace == nspace).ToArray();
            //_db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
            ////初始化炒鸡管理员和菜单
            //Users user = new Users()
            //{
            //    Name = "admin",
            //    NickName = "炒鸡管理员",
            //    Password = "123456",
            //    UserType = 0,
            //    IsEnable = true,
            //    Description = "数据库初始化时默认添加的炒鸡管理员",
            //    CreateDate = DateTime.Now,
            //    CreateUserId = 0,
            //    IsDeleted = 0
            //};
            //long userId = _db.Insertable(user).ExecuteReturnBigIdentity();
            //Menu menuRoot = new Menu()
            //{
            //    Name = "菜单管理",
            //    Index = "menumanager",
            //    FilePath = "../views/admin/menu/MenuManager",
            //    ParentId = 0,
            //    Order = 0,
            //    IsEnable = true,
            //    Description = "数据库初始化时默认添加的默认菜单",
            //    CreateDate = DateTime.Now,
            //    CreateUserId = userId,
            //    IsDeleted = 0
            //};
            //_db.Insertable(menuRoot).ExecuteReturnBigIdentity();
            return res;
          
        }
        [HttpGet]
        public string Test()
        {
            return "OKK";
        }
    }
}
