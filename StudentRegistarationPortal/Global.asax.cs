using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using BusinessLogicLayer.Repository;
using DataAccessLayer.Context;
using StudentRegistarationPortal.Services;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentRegistarationPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Initialize AutoMapper
            InitializeAutoMapper();

            // Register dependencies with Autofac
            RegisterDependencies();
        }

        private void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperWebProfile>();
            });
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // Register controllers in the current assembly
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register our DbContext
            builder.RegisterType<StudentRPDbContext>().InstancePerRequest();

            // Register repositories and other services
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerRequest();

            // Register AutoMapper configuration
            builder.Register(context =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutoMapperWebProfile>(); 
                });
                return config.CreateMapper();
            }).As<IMapper>().InstancePerLifetimeScope();

            // Set the dependency resolver to be Autofac
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
