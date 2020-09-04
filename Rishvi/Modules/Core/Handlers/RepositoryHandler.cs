using Autofac;
using AutoMapper;
using Rishvi.Modules.Core.Data;
using Rishvi.Modules.Users.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Core.Handlers
{
    internal class RepositoryHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("MappingProfile"))
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
                   .Where(t => t.Name.EndsWith("Validator"))
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Notification"))
                .InstancePerLifetimeScope();
        }
    }
}
