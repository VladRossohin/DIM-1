﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DIMS.EF.DAL.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Data.Common;
    using System.Data;
    using System.Data.Entity.Core.EntityClient;

    public partial class DIMSDBContext : DbContext
    {

        private readonly DbProviderFactory _factoryToUse = DbProviderFactories.GetFactory("System.Data.SqlClient");

        public DIMSDBContext()
            : base("name=DIMSDBConnection")
        {
        }

        /// <summary>
        ///this constructor is needed only for Effort, as attachment point 
        ///we will pass connection, fetched from this library as argument
        /// </summary>
        /// <param name="connection">enable connection to context</param>

        public DIMSDBContext(DbConnection connection) : base(connection, true)
        {
        }


        public DIMSDBContext(string connectionString) 
            : base(connectionString)
        {

        }
    
       
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Sample> Samples { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskState> TaskStates { get; set; }
        public virtual DbSet<TaskTrack> TaskTracks { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }
        public virtual DbSet<VTask> vTasks { get; set; }
        public virtual DbSet<vUserProfile> vUserProfiles { get; set; }
        public virtual DbSet<vUserProgress> vUserProgresses { get; set; }
        public virtual DbSet<vUserTask> vUserTasks { get; set; }
        public virtual DbSet<vUserTrack> vUserTracks { get; set; }
    
        public virtual int SampleEntriesAmount(Nullable<bool> isAdmin, ObjectParameter result)
        {
            var isAdminParameter = isAdmin.HasValue ?
                new ObjectParameter("isAdmin", isAdmin) :
                new ObjectParameter("isAdmin", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SampleEntriesAmount", isAdminParameter, result);
        }


        /// <summary>Calls the stored procedure '[dbo].[SampleEntriesAmount]'</summary>
        /// <param name="isAdmin">Parameter mapped onto the stored procedure parameter '@isAdmin'</param>
        /// <param name="result">Parameter mapped onto the stored procedure parameter '@result'</param>
        /// <returns>The number of rows affected, as reported by ADO.NET</returns>
        public int GetSampleEntriesAmount(System.Boolean isAdmin, ref System.Int32 result)
        {
            var cmd = CreateStoredProcCallCommand("[dbo].[SampleEntriesAmount]");
            AddParameter(cmd, "@isAdmin", 0, ParameterDirection.Input, isAdmin);
            AddParameter(cmd, "@result", 0, ParameterDirection.InputOutput, result);
            var toReturn = ExecuteNonQueryCommand(cmd);
            result = GetParameterValue<System.Int32>(cmd.Parameters[1].Value);
            return toReturn;
        }

        /// <summary>Adds a new parameter created from the input specified to the command specified</summary>
        /// <param name="cmd">The command to add the new parameter to</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="length">The length.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="value">The value for the parameter</param>
        private static void AddParameter(DbCommand cmd, string parameterName, int length, ParameterDirection direction, object value)
        {
            var dummyParam = new EntityParameter() { Value = value };
            var parameter = cmd.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Direction = direction;
            parameter.Size = length;
            parameter.Value = value;
            parameter.DbType = dummyParam.DbType;
            cmd.Parameters.Add(parameter);
        }

        /// <summary>Executes the passed in command</summary>
        /// <param name="cmd">command to execute</param>
        /// <returns>Value returned by ExecuteNonQuery</returns>
        private static int ExecuteNonQueryCommand(DbCommand cmd)
        {
            bool openedLocally = false;
            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
                openedLocally = true;
            }
            var toReturn = cmd.ExecuteNonQuery();
            if (openedLocally)
            {
                cmd.Connection.Close();
            }
            return toReturn;
        }

        /// <summary>Creates a new stored procedure call command and sets it up to make it ready to use.</summary>
        /// <param name="storedProcedureToCall">The stored procedure to call.</param>
        /// <returns>ready to use command</returns>
        private DbCommand CreateStoredProcCallCommand(string storedProcedureToCall)
        {
            var cmd = _factoryToUse.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storedProcedureToCall;
            return SetupCommand(cmd);
        }

        /// <summary>Sets up the command specified. It wires it to the current connection and transaction, sets command timeout and if requested, it also opens the command</summary>
        /// <param name="toSetup">The command to setup</param>
        /// <returns>The passed in DbCommand</returns>
        private DbCommand SetupCommand(DbCommand toSetup)
        {
            if (toSetup == null)
            {
                return toSetup;
            }
            toSetup.Connection = GetConnection();
            toSetup.CommandTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout ?? 0;
            return toSetup;
        }

        /// <summary>Obtains the live DbConnection used by this context</summary>
        /// <returns>Live DbConnection used by this context</returns>
        private DbConnection GetConnection()
        {
            return ((EntityConnection)((IObjectContextAdapter)this).ObjectContext.Connection).StoreConnection;
        }

        /// <summary>Gets the real value from the raw parameter value specified and DBNull.Value values to null.</summary>
        /// <typeparam name="T">the expected type of the value of the parameter</typeparam>
        /// <param name="parameterValue">The raw parameter value.</param>
        /// <returns>the properly converted value from the raw parameter value specified</returns>
        private static T GetParameterValue<T>(object parameterValue)
        {
            return Convert.IsDBNull(parameterValue) ? default(T) : (T)parameterValue;
        }
    }
}
