using DIMS.BL.Interfaces;
using DIMS.BL.Models;
using DIMS.BL.Services;
using DIMS.Email.Services;
using Email.Interfaces;
using Ninject.Modules;
using NLog;
using System.Configuration;

namespace DIMS.Server.utils
{
    public class DependencesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISampleService>().To<SampleService>();
            Bind<IAuthService<UserDTO>>().To<UserAuthService>();
            Bind<IUserService>().To<UserService>();
            Bind<IUserProfileService>().To<UserProfileService>();
            Bind<IUserTaskService>().To<UserTaskService>();
            Bind<ITaskService>().To<TaskService>();
            Bind<IDirectionService>().To<DirectionService>();
            Bind<ITaskStateService>().To<TaskStateService>();
            Bind<ITaskTrackService>().To<TaskTrackService>();


            Bind<IVTaskService>().To<VTaskService>();
            Bind<IVTaskStateService>().To<VTaskStateService>();
            Bind<IVUserProfileService>().To<VUserProfileService>();
            Bind<IVUserProgressService>().To<VUserProgressService>();
            Bind<IVUserTaskService>().To<VUserTaskService>();
            Bind<IVUserTrackService>().To<VUserTrackService>();
            Bind<IVTaskTrackService>().To<VTaskTrackService>();

            Bind<ISender>().To<Sender>()
                .InSingletonScope()
            .WithConstructorArgument("apiKey", ConfigurationManager.AppSettings["apiKey"])
            .WithConstructorArgument("email", ConfigurationManager.AppSettings["email"]);
        }
    }
}