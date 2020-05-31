using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using DIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DIMS.Server.Controllers.Menthor
{
    public class TasksManageController : AbstractController
    {
        public TasksManageController(ITaskService taskService, IVTaskService vTaskService, IUserTaskService userTaskService,
            IVUserProfileService vUserProfileService, IVTaskStateService vTaskStateService, IVUserTaskService vUserTaskService, IMapper mapper)
            : base(taskService, vTaskService, userTaskService, vUserProfileService, vTaskStateService, vUserTaskService, mapper)
        {
        }

        [Authorize(Roles = "admin, mentor")]
        [Route("tasks")]
        public ActionResult Index()
        {
            IEnumerable<VTaskDTO> taskDTOs = _vTaskService.GetAll();

            var taskListViewModel = _mapper.Map<IEnumerable<VTaskDTO>, IEnumerable<vTaskViewModel>>(taskDTOs);

            return View(taskListViewModel);
        }

        public ActionResult Create()
        {
            var userProfileDtos = _vUserProfileService.GetAll();
            var userProfileListViewModel = _mapper.Map<IEnumerable<VUserProfileDTO>, IEnumerable<VUserProfileViewModel>>(userProfileDtos);

            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel
            {
                userProfileListViewModel = userProfileListViewModel
            };

            return View(taskManagePageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskViewModel taskViewModel, UserTaskViewModel userTaskViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var taskDto = _mapper.Map<TaskViewModel, TaskDTO>(taskViewModel);
                    var userProfileDtos = _vUserProfileService.GetAll();
                    var userProfileListViewModel = _mapper.Map<IEnumerable<VUserProfileDTO>, IEnumerable<VUserProfileViewModel>>(userProfileDtos);

                    IEnumerable<string> selectedUsers = Request.Form.GetValues("SelectedUserProfiles");
                    int activeState = 1;

                    if (selectedUsers != null)
                    {
                        foreach (var userId in selectedUsers)
                        {
                            var userTaskDto = _mapper.Map<UserTaskViewModel, UserTaskDTO>(userTaskViewModel);

                            userTaskDto.UserId = Convert.ToInt32(userId);
                            userTaskDto.StateId = activeState;
                            userTaskDto.TaskId = taskDto.TaskId;

                            taskDto.UserTasks.Add(userTaskDto);
                        }
                    }

                    _taskService.Save(taskDto);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel();
            return View(taskManagePageViewModel);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.TaskUsers = GetUsersForTask(id);

            var taskDto = _taskService.GetById(id);
            var userProfileDtos = _vUserProfileService.GetAll();

            if (taskDto == null)
            {
                return HttpNotFound();
            }

            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel
            {
                userProfileListViewModel = _mapper.Map<IEnumerable<VUserProfileDTO>, IEnumerable<VUserProfileViewModel>>(userProfileDtos),
                taskViewModel = _mapper.Map<TaskDTO, TaskViewModel>(taskDto)
            };

            return View(taskManagePageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskManagePageViewModel taskManagePageViewModel, int id)
        {
            var taskDto = _mapper.Map<TaskViewModel, VTaskDTO>(taskManagePageViewModel.taskViewModel);
            taskDto.TaskId = id;

            var newUsers = new List<string>();
            var oldUsers = GetUsersForTask(id);

            IEnumerable<string> selectedUsers = Request.Form.GetValues("SelectedUsers");
            int activeState = 1;

            if (selectedUsers != null)
            {
                foreach (var item in selectedUsers)
                {
                    newUsers.Add(item);
                }
            }

            var UsersForDeleteTask = oldUsers.Except(newUsers);
            var UsersForAddTask = newUsers.Except(oldUsers);

            if (TryUpdateModel(taskDto))
            {
                try
                {
                    _vTaskService.Update(taskDto);
                    foreach (var item in UsersForAddTask)
                    {
                        var userTask = new UserTaskViewModel
                        {
                            UserId = Convert.ToInt32(item),
                            TaskId = taskDto.TaskId,
                            StateId = activeState
                        };

                        var userTaskDto = _mapper.Map<UserTaskViewModel, VUserTaskDTO>(userTask);
                        _vUserTaskService.Save(userTaskDto);
                    }

                    foreach (var userId in UsersForDeleteTask)
                    {
                        _userTaskService.DeleteItemByTaskIdAndUserId(taskDto.TaskId, Convert.ToInt32(userId));
                    }

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return PartialView(taskManagePageViewModel.taskViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var taskDto = _taskService.GetById(id);
            var userTaskDtos = _taskService.GetUserTasks(id);
            var userProfileDtos = _vUserProfileService.GetAll();
            var taskStateDtos = _vTaskStateService.GetAll();

            if (taskDto == null)
            {
                return HttpNotFound();
            }

            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel
            {
                userProfileListViewModel = _mapper.Map<IEnumerable<VUserProfileDTO>, IEnumerable<VUserProfileViewModel>>(userProfileDtos),
                taskViewModel = _mapper.Map<TaskDTO, TaskViewModel>(taskDto),
                userTaskListViewModel = _mapper.Map<IEnumerable<UserTaskDTO>, IEnumerable<UserTaskViewModel>>(userTaskDtos),
                taskStateListViewModel = _mapper.Map<IEnumerable<TaskStateDTO>, IEnumerable<TaskStateViewModel>>(taskStateDtos)
            };

            return View(taskManagePageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            try
            {
                _taskService.DeleteById(id);
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskDto = _taskService.GetById(id.Value);
            var userProfileDtos = _vUserProfileService.GetAll();
            var userTaskDtos = _userTaskService.GetAllUserTasksByTaskId(id.Value);

            if (taskDto == null)
            {
                return HttpNotFound();
            }

            TaskManagePageViewModel taskManagePageViewModel = new TaskManagePageViewModel
            {
                taskViewModel = _mapper.Map<TaskDTO, TaskViewModel>(taskDto),
                userTaskListViewModel = _mapper.Map<IEnumerable<UserTaskDTO>, IEnumerable<UserTaskViewModel>>(userTaskDtos),
                userProfileListViewModel = _mapper.Map<IEnumerable<VUserProfileDTO>, IEnumerable<VUserProfileViewModel>>(userProfileDtos)
            };

            return PartialView(taskManagePageViewModel);
        }
    }
}