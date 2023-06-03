using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlSugar;
using System.Runtime.CompilerServices;

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
            
            });
        }
        }
}
