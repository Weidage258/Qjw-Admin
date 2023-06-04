# QJWDamin 管理系统

# 后端

运用技术 NET6,0 、NET CORE、 sqlSugar、AutoMapper、dto映射、ioc控制反转，完成的权限管理系统  JWT的使用 Log4net 反射



第一步创建 WEB Core Api 项目

添加文件夹config 下创建HostBuilderExtend.cs 

```
  public static class HostBuilderExtend
    {
        public static void Register(this WebApplicationBuilder app) {
            //UseServiceProviderFactory重写用于创建服务提供程序的工厂
            app.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory);
            app.Host.ConfigureContainer<ContainerBuilder>(builder => {
                #region 注册sqlsugar
                builder.Register<ISqlSugarClient>(context =>
                {
                    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                    {
                        //连接字符串
                        ConnectionString = "Data Source=.;Initial Catalog=ZhaoxiAdminDb1;Persist Security Info=True;User ID=sa;Password=123456",
                        DbType = DbType.SqlServer,
                        IsAutoCloseConnection = true
                    });
                    return db;
                });
                #endregion
            });
        }
    }
```



第二步安装 nuget包 sqlSugarCore包，和AutoMapper,Extension,microsoft,dependencyinjection包

Program 下 在var app = builder.Build(); 前面

 注册builder.Register();

```
  public static class HostBuilderExtend
    {
        public static void Register(this WebApplicationBuilder app) {
            //UseServiceProviderFactory重写用于创建服务提供程序的工厂
            app.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory);
            app.Host.ConfigureContainer<ContainerBuilder>(builder => {
                #region 注册sqlsugar
                builder.Register<ISqlSugarClient>(context =>
                {
                    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                    {
                        //连接字符串
                        ConnectionString = "Data Source=.;Initial Catalog=ZhaoxiAdminDb1;Persist Security Info=True;User ID=sa;Password=123456",
                        DbType = DbType.SqlServer,
                        IsAutoCloseConnection = true
                    });
                    return db;
                });
                #endregion
            });
        }
    }
```

Host层创建Controller ToolController

## Swagger API运用方法创建种子数据 通过映射获取数据

```c#

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
            string nspace = "Model.Entitys";
            //通过反射读取我们想要的类
            //获取当前路径的model.dll，获取想要的类
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll").GetTypes().Where(p => p.Namespace == nspace).ToArray();
           //默认长度两百
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
            //初始威大哥管理员和菜单
            Users user = new Users()
            {
                Name = "admin",
                NickName = "威大哥管理员",
                Password = "123456",
                UserType = 0,
                IsEnable = true,
                Description = "数据库初始化时默认添加的炒鸡管理员",
                CreateDate = DateTime.Now,
                CreateUserId = 0,
                IsDeleted = 0
            };
            long userId = _db.Insertable(user).ExecuteReturnBigIdentity();
            Menu menuRoot = new Menu()
            {
                Name = "菜单管理",
                Index = "menumanager",
                FilePath = "../views/admin/menu/MenuManager",
                ParentId = 0,
                Order = 0,
                IsEnable = true,
                Description = "数据库初始化时默认添加的默认菜单",
                CreateDate = DateTime.Now,
                CreateUserId = userId,
                IsDeleted = 0
            };
            _db.Insertable(menuRoot).ExecuteReturnBigIdentity();
            return res;
          
        }
```

## 接口层和实现层绑定以及注入

一、 新建接口层和实现层类库

二、安装Autofac

Autofac

Autofac.Extensions.DependencyInjection

三、webapi新建config文件夹，新建AutofacModuleRegister文件

四、Program注册 

builder.RegisterModule(new AutofacModuleRegister());

## AutoMapper的介绍和使用

一、背景



  在实际的项目开发过程中，经常会涉及到传输实体到模型实体之间的转换，通过属性的逐个赋值我们可以将传入的参数传递给另外一个实体对象。

  但是随着业务复杂度的提升，有些实体的属性高达几十或上百个，那么逐个赋值会增加代码量且不美观，那么有没有一种方法， 可以实现实体到实体之间属性的映射呢，AutoMapper应运而生。



二、使用



  1、引入Nuget包 AutoMapper、AutoMapper.Extensions.Microsoft.DependencyInjection

  2、新建Config文件夹，以及AutoMapperConfigs文件，引用Profile

  3、在构造函数中管理映射关系：CreateMap<Users, UserRes>();

  4、在Program中注册

  builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

  5、使用

  return _mapper.Map<UserRes>(user);

```c#
  //注册接口和实现之间的关系
        protected override void Load(ContainerBuilder builder) {
            Assembly interfaceAssembly = Assembly.Load("Interface");
            Assembly serviceAssembly = Assembly.Load("Service");
            //RegisterAssemblyTypes非静态的公开类型被批量注册 .AsImplementedInterfaces()  是以接口方式进行注入,注入这些类的所有的公共接口作为服务
            builder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly).AsImplementedInterfaces();

        }
```
 ## JWT的使用
 1.引入Microsoft.AspNetCore.Authentication.JwtBearer

2.搭建认证服务（Model，抽象类，实现）提供生成Token方法

3.注册JWT
app.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));

4.appsettings.json 添加配置)


