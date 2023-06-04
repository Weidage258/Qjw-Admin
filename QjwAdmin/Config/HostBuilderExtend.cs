using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlSugar;
using System.Runtime.CompilerServices;
using AutoMapper;
using Model.Other;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace QjwAdmin.Config
{
        public static class HostBuilderExtend
        {
            public static void Register(this WebApplicationBuilder builder) {
            //UseServiceProviderFactory重写用于创建服务提供程序的工厂
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //注册后才能使用sqlsugar
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                #region 注册sqlsugar
                builder.Register<ISqlSugarClient>(context =>
                {
                    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                    {
                        //连接字符串
                        ConnectionString = "Data Source=.;Initial Catalog=QjwAdminDb;Persist Security Info=True;User ID=sa;Password=123456",
                        
                        DbType = DbType.SqlServer,
                        IsAutoCloseConnection = true
                    });
                    //支持sql语句的输出，方便排查问题
                    db.Aop.OnLogExecuted = (sql, par) =>
                    {
                        Console.WriteLine("\r\n");
                        Console.WriteLine($"{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}，Sql语句：{sql}");
                        Console.WriteLine("===========================================================================");
                    };

                    return db;
                });
                #endregion

                //注册接口和实现层
                builder.RegisterModule(new AutofacModuleRegister());
          
            });
            //Automapper映射
            builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));
            //第一步，注册JWT
            builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));
            #region JWT校验
            //第二步，增加鉴权逻辑
            JWTTokenOptions tokenOptions = new JWTTokenOptions();
            builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
             .AddJwtBearer(options =>  //这里是配置的鉴权的逻辑
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     //JWT有一些默认的属性，就是给鉴权时就可以筛选了
                     ValidateIssuer = true,//是否验证Issuer
                     ValidateAudience = true,//是否验证Audience
                     ValidateLifetime = true,//是否验证失效时间
                     ValidateIssuerSigningKey = true,//是否验证SecurityKey
                     ValidAudience = tokenOptions.Audience,//
                     ValidIssuer = tokenOptions.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))//拿到SecurityKey 
                 };
             });
            #endregion
        }

    }
}
