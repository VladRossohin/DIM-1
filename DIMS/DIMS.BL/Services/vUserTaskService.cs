﻿using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using DIMS.EF.DAL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.BL.Services
{
    public class vUserTaskService : IvUserTaskService
    {

        private readonly IUnitOfWork Database;
        private readonly vUserTaskRepository Repository;

        public vUserTaskService(IUnitOfWork uow, vUserTaskRepository repository)
        {
            Database = uow;
            Repository = repository;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<vUserTaskDTO> GetByUserId(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The view user task id value is not set", String.Empty);
            }

            return Mapper.Map<IEnumerable<vUserTask>, IEnumerable<vUserTaskDTO>>(
                Repository.GetByUserId(id.Value));
        }

        public vUserTaskDTO GetById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The view user task id value is not set", String.Empty);
            }

            var _vUserTask = Database.vUserTasks.GetById(id.Value);

            if (_vUserTask == null)
            {
                throw new ValidationException($"The view user task with id = {id.Value} was not found", String.Empty);
            }

            return Mapper.Map<vUserTask, vUserTaskDTO>(_vUserTask);
        }

        public IEnumerable<vUserTaskDTO> GetAll()
        {
            return Mapper.Map<List<vUserTask>, ICollection<vUserTaskDTO>>(
                Database.vUserTasks.GetAll().ToList());
        }
    }
}
