using Autofac;
using System.Reflection;

namespace QjwAdmin.Config
{
    public class AutofacModuleRegister:Autofac.Module
    {
        //注册接口和实现之间的关系
        protected override void Load(ContainerBuilder builder) {
            Assembly interfaceAssembly = Assembly.Load("Interface");
            Assembly serviceAssembly = Assembly.Load("Service");
            //RegisterAssemblyTypes非静态的公开类型被批量注册 .AsImplementedInterfaces()  是以接口方式进行注入,注入这些类的所有的公共接口作为服务
            builder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly).AsImplementedInterfaces();

        }
    }
}
