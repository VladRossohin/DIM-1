﻿using DIMS.BL.Interfaces;
using DIMS.BL.Models;
using DIMS.BL.Services;
using DIMS.Email.Services;
using Email.Interfaces;
using Ninject.Modules;
using System.Configuration;

namespace DIMS.Server.utils
{
    public class DependencesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISampleService>().To<SampleService>();
            Bind<IUserService>().To<UserService>();
            Bind<IUserProfileService>().To<UserProfileService>();
            Bind<IUserTaskService>().To<UserTaskService>();
            Bind<ITaskService>().To<TaskService>();
            Bind<IDirectionService>().To<DirectionService>();
            Bind<ITaskStateService>().To<TaskStateService>();
            Bind<ITaskTrackService>().To<TaskTrackService>();

            Bind<IvTaskService>().To<vTaskService>();
            Bind<IvUserProfileService>().To<vUserProfileService>();
            Bind<IvUserProgressService>().To<vUserProgressService>();
            Bind<IvUserTaskService>().To<vUserTaskService>();
            Bind<IvUserTrackService>().To<vUserTrackService>();
            Bind<IAuthService<UserDTO>>().To<UserAuthService>();

            Bind<ISender>().To<Sender>()
                .InSingletonScope()
            .WithConstructorArgument("apiKey", ConfigurationManager.AppSettings["apiKey"])
            .WithConstructorArgument("email", ConfigurationManager.AppSettings["email"]);
        }
    }
}