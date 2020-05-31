using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using DIMS.EF.DAL.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS.BL.Services
{
    public class UserProfileService : IUserProfileService
    {
        private IUnitOfWork Database { get; }
        private UserService UserService { get; }
        private UserProfileRepository Repository { get; }
        private UserTaskService UserTasks { get; }

        private readonly IMapper _mapper;

        public UserProfileService(IUnitOfWork unitOfWork,
                                  UserProfileRepository userProfileRepository,
                                  UserService userService,
                                  UserTaskService userTaskService,
                                  IMapper mapper)
        {
            Database = unitOfWork;
            Repository = userProfileRepository;
            UserService = userService;
            UserTasks = userTaskService;
            _mapper = mapper;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The User Profile's id value is not set", string.Empty);
            }

            Database.UserProfiles.DeleteById(id.Value);
            Database.Save();
        }

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            if (email == null)
            {
                throw new ValidationException("The User Profile's email is not set", string.Empty);
            }

            OperationDetails operationDetails = await UserService.DeleteByEmail(email);

            // TODO: create enum for general messages or rewrite this checking
            if (operationDetails.Succedeed || operationDetails.Message == "The user with such Email not found! Email: ")
            {
                Repository.DeleteByEmail(email);

                return true;
            }
            else
            {
                throw new ValidationException(operationDetails.Message, operationDetails.Property);
            }
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The User Profile's id value is not set!", string.Empty);
            }

            var userProfileEmail = Database.UserProfiles.GetById(id.Value).Email;

            return await DeleteByEmailAsync(userProfileEmail);
        }

        public UserProfileDTO GetById(int id)
        {
            var userProfile = Database.UserProfiles.GetById(id);

            if (userProfile == null)
            {
                throw new ValidationException($"The User Profile with id = {id} was not found", string.Empty);
            }

            return _mapper.Map<UserProfile, UserProfileDTO>(userProfile);
        }

        public IEnumerable<UserProfileDTO> GetAll()
        {
            var userProfiles = Database.UserProfiles.GetAll();

            return _mapper.Map<IEnumerable<UserProfile>, List<UserProfileDTO>>(userProfiles);
        }

        public void Save(UserProfileDTO userProfile)
        {
            // TODO: improve validation - maybe create validation service with methods
            if (userProfile.Name.Length > 25)
            {
                throw new ValidationException($"The length of {nameof(userProfile.Name)} must be less than 25"
                    , nameof(userProfile.Name));
            }

            if (userProfile.LastName.Length > 25)
            {
                throw new ValidationException($"The length of {nameof(userProfile.LastName)} must be less than 25"
                    , nameof(userProfile.LastName));
            }

            if (userProfile.Address != null && userProfile.Address.Length > 255)
            {
                throw new ValidationException($"The length of {nameof(userProfile.Address)} must be less than 25"
                    , nameof(userProfile.Address));
            }

            var _userProfile = new UserProfile
            {
                Name = userProfile.Name,
                Email = userProfile.Email,
                LastName = userProfile.LastName,
                DirectionId = userProfile.DirectionId,
                Sex = userProfile.Sex,
                Education = userProfile.Education,
                BirthDate = userProfile.BirthDate,
                UniversityAverageScore = userProfile.UniversityAverageScore,
                MathScore = userProfile.MathScore,
                Address = userProfile.Address,
                MobilePhone = userProfile.MobilePhone,
                Skype = userProfile.Skype,
                StartDate = userProfile.StartDate
            };

            Database.UserProfiles.Create(_userProfile);
            Database.Save();
        }

        public void Update(UserProfileDTO userProfile)
        {
            // TODO: improve validation - maybe create validation service with methods
            if (userProfile.Name.Length > 25)
            {
                throw new ValidationException($"The length of {nameof(userProfile.Name)} must be less than 25"
                    , nameof(userProfile.Name));
            }

            if (userProfile.LastName.Length > 25)
            {
                throw new ValidationException($"The length of {nameof(userProfile.LastName)} must be less than 25"
                    , nameof(userProfile.LastName));
            }

            if (userProfile.Address.Length > 255)
            {
                throw new ValidationException($"The length of {nameof(userProfile.Address)} must be less than 25"
                    , nameof(userProfile.Address));
            }

            var _userProfile = Database.UserProfiles.GetById(userProfile.UserId);

            var userTasks = UserTasks.GetByUserId(userProfile.UserId);

            if (_userProfile != null)
            {
                _mapper.Map(userProfile, _userProfile);

                _userProfile.UserTasks = _mapper.Map<IEnumerable<UserTaskDTO>, ICollection<UserTask>>(userTasks);

                Database.Save();
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
