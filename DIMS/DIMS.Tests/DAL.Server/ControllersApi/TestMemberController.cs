using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using DIMS.Server.ControllersApi;
using DIMS.Server.Models;
using DIMS.Server.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace HIMS.Tests.Controller
{
    [TestClass]
    public class TestMemberController
    {
        private Mock<IMapper> _mapper = new Mock<IMapper>();

        private Mock<IUserProfileService> _mockUserProfileService;
        private Mock<IVUserProfileService> _mockVUserProfileService;
        private Mock<IDirectionService> _mockDirectionService;

        private List<UserProfileDTO> userProfileDTOs;

        private AutoMapperModule autoMapperModule = new AutoMapperModule();

        private MemberController _controller;

        [SetUp]
        public void InitialSetup()
        {
            _mockUserProfileService = new Mock<IUserProfileService>();
            _mockVUserProfileService = new Mock<IVUserProfileService>();
            _mockDirectionService = new Mock<IDirectionService>();

            _controller = new MemberController(_mockUserProfileService.Object, _mockVUserProfileService.Object, _mockDirectionService.Object, _mapper.Object);

            userProfileDTOs = new List<UserProfileDTO>
            {
                new UserProfileDTO
                {
                    UserId = 1,
                    Address = "Address 1",
                    BirthDate = new DateTime(2000, 1, 29),
                    DirectionId = 1,
                    Education = "Ed 1",
                    Email = "Email 1",
                    LastName = "LastName 1",
                    Name = "Name 1",
                    MathScore = 9,
                    UniversityAverageScore = 9,
                    MobilePhone = "+111111111111",
                    Sex = "M",
                    Skype = "Skype 1",
                    StartDate = new DateTime(2020, 3, 9)
                },
                new UserProfileDTO
                {
                    UserId = 2,
                    Address = "Address 2",
                    BirthDate = new DateTime(2000, 1, 29),
                    DirectionId = 2,
                    Education = "Ed 2",
                    Email = "Email 2",
                    LastName = "LastName 2",
                    Name = "Name 2",
                    MathScore = 9,
                    UniversityAverageScore = 9,
                    MobilePhone = "+222222222222",
                    Sex = "F",
                    Skype = "Skype 2",
                    StartDate = new DateTime(2020, 3, 9)
                }
            };


        }

        [TestMethod]
        public void CreateUser_ShouldCreateUser()
        {
            autoMapperModule.Load();

            _mockUserProfileService = new Mock<IUserProfileService>();
            _mockVUserProfileService = new Mock<IVUserProfileService>();
            _mockDirectionService = new Mock<IDirectionService>();

            var userProfileDtos = GetTestUserProfileDtos();

            var user = new UserProfileViewModel
            {
                UserId = 3,
                Address = "Address 3",
                BirthDate = new DateTime(2000, 1, 29),
                DirectionId = 3,
                Education = "Ed 3",
                Email = "Email 3",
                LastName = "LastName 3",
                Name = "Name 3",
                MathScore = 9,
                UniversityAverageScore = 9,
                MobilePhone = "+333333333333",
                Sex = "M",
                Skype = "Skype 3",
                StartDate = new DateTime(2020, 3, 9)
            };


            _mapper.Setup(m => m.Map<UserProfileViewModel, UserProfileDTO>(user)).Returns(new UserProfileDTO
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                UserId = user.UserId,
                Address = user.Address,
                BirthDate = user.BirthDate.Value,
                DirectionId = user.DirectionId,
                Education = user.Education,
                MathScore = user.MathScore,
                MobilePhone = user.MobilePhone,
                Sex = user.Sex,
                Skype = user.Skype,
                StartDate = user.StartDate.Value,
                UniversityAverageScore = user.UniversityAverageScore
            });

            var userDto = _mapper.Object.Map<UserProfileViewModel, UserProfileDTO>(user);

            _mockUserProfileService.Setup(s => s.Save(userDto)).Callback(() => this.userProfileDTOs.Add(userDto));

            _controller = new MemberController(_mockUserProfileService.Object, _mockVUserProfileService.Object, _mockDirectionService.Object, _mapper.Object);
            
            var result = _controller.Create(user) as NegotiatedContentResult<UserProfileViewModel>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);

            _mockUserProfileService.Setup(service => service.GetById(3)).Returns(userDto);

            _controller = new MemberController(_mockUserProfileService.Object, _mockVUserProfileService.Object, _mockDirectionService.Object, _mapper.Object);

            var getResult = _controller.GetUser(3) as OkNegotiatedContentResult<UserProfileViewModel>;

            Assert.IsNotNull(getResult);
        }

        [TestMethod]
        public void GetUser_ShouldReturnCorrectUser()
        {
            autoMapperModule.Load();

            _mockUserProfileService = new Mock<IUserProfileService>();
            _mockVUserProfileService = new Mock<IVUserProfileService>();
            _mockDirectionService = new Mock<IDirectionService>();

            userProfileDTOs = GetTestUserProfileDtos();

            _mockUserProfileService.Setup(service => service.GetById(1)).Returns(userProfileDTOs.Where(user => user.UserId == 1).FirstOrDefault());
    
            _controller = new MemberController(_mockUserProfileService.Object, _mockVUserProfileService.Object, _mockDirectionService.Object, _mapper.Object);

            var result = _controller.GetUser(1) as OkNegotiatedContentResult<UserProfileViewModel>;

            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Content);
            Assert.AreEqual(1, result.Content.UserId);
        }

        private List<UserProfileDTO> GetTestUserProfileDtos()
        {
            var testUserProfiles = new List<UserProfileDTO>();

            testUserProfiles.Add(new UserProfileDTO
            {
                UserId = 1,
                Address = "Address 1",
                BirthDate = new DateTime(2000, 1, 29),
                DirectionId = 1,
                Education = "Ed 1",
                Email = "Email 1",
                LastName = "LastName 1",
                Name = "Name 1",
                MathScore = 9,
                UniversityAverageScore = 9,
                MobilePhone = "+111111111111",
                Sex = "M",
                Skype = "Skype 1",
                StartDate = new DateTime(2020, 3, 9)
            }) ;
            testUserProfiles.Add(new UserProfileDTO
            {
                UserId = 2,
                Address = "Address 2",
                BirthDate = new DateTime(2000, 1, 29),
                DirectionId = 2,
                Education = "Ed 2",
                Email = "Email 2",
                LastName = "LastName 2",
                Name = "Name 2",
                MathScore = 9,
                UniversityAverageScore = 9,
                MobilePhone = "+222222222222",
                Sex = "F",
                Skype = "Skype 2",
                StartDate = new DateTime(2020, 3, 9)
            });

            return testUserProfiles;
        }
    }
}
