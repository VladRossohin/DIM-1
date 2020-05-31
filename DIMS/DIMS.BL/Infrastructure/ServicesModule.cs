using AutoMapper;
using DIMS.EF.DAL.Data;
using DIMS.EF.DAL.Data.Interfaces;
using DIMS.EF.DAL.Data.Repositories;
using DIMS.EF.DAL.Identity.Repositories;
using Ninject;
using Ninject.Modules;

namespace DIMS.BL.Infrastructure
{
    public class ServicesModule : NinjectModule
    {
        private readonly string _connectionString;
        private readonly string _identityConnectionString;

        public ServicesModule(string connectionString, string identityConnectionString)
        {
            _connectionString = connectionString;
            _identityConnectionString = identityConnectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(_connectionString);
            Bind<DIMS.EF.DAL.Identity.Interfaces.IUnitOfWork>().To<IdentityUnitOfWork>().WithConstructorArgument(_identityConnectionString);
            Bind<IProcedureManager>().To<ProcedureManager>().WithConstructorArgument(_connectionString);
        }
    }
}
