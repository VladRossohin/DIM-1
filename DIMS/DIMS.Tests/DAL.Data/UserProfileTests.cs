using DIMS.EF.DAL.Data;
using DIMS.EF.DAL.Data.Repositories;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.EntityClient;

namespace DIMS.Tests.DAL.Data
{
    [TestFixture]
    public class UserProfileTests
    {
        private DIMSDBContext context;
        private EntityConnection connection;

        private EntityConnection CreateConnection()
        {
            return Effort.EntityConnectionFactory.CreateTransient("name=FakeDIMSDBConnection");
        }

        private DIMSDBContext CreateContext(EntityConnection connectionString)
        {
            return new DIMSDBContext(connectionString);
        }

        private UserProfileRepository CreateRepository(DIMSDBContext context)
        {
            return new UserProfileRepository(context);
        }

        private UserProfileRepository SeedUserProfileRepository()
        {
            context.UserProfiles.AddRange(
               new List<UserProfile>
               {
               new UserProfile {
                    UserId = 1, Name = "username1", LastName = "userLname", Sex = "male", DirectionId = 2,
                    Education = "Harvard", Email = "email", UniversityAverageScore = 6.5d, MathScore = 9.0d,
                    MobilePhone = "+375(33)1922", Skype = "skype", StartDate = System.DateTime.Now,
                    BirthDate = System.DateTime.Now, Address = "dom 5" },
               new UserProfile {
                    UserId = 2, Name = "username2", LastName = "userLname", Sex = "male", DirectionId = 2,
                    Education = "Harvard", Email = "email", UniversityAverageScore = 6.5d, MathScore = 9.0d,
                    MobilePhone = "+375(33)1922", Skype = "skype", StartDate = System.DateTime.Now,
                    BirthDate = System.DateTime.Now, Address = "dom 5" },
               new UserProfile {
                    UserId = 3, Name = "username3", LastName = "userLname", Sex = "male", DirectionId = 2,
                    Education = "Harvard", Email = "email", UniversityAverageScore = 6.5d, MathScore = 9.0d,
                    MobilePhone = "+375(33)1922", Skype = "skype", StartDate = System.DateTime.Now,
                    BirthDate = System.DateTime.Now, Address = "dom 5" }
               }
               );
            context.SaveChanges();

            var repo = CreateRepository(context);

            return repo;
        }

        [SetUp]
        public void Initialize()
        {
            connection = CreateConnection();
            context = CreateContext(connection);
        }

        [Test]
        public void ShouldCreateUserProfile()
        {
            //Arrange
            var repo = CreateRepository(context);
            var user = new UserProfile
            {
                UserId = 5,
                Name = "username",
                LastName = "userLname",
                Sex = "male",
                DirectionId = 2,
                Education = "Harvard",
                Email = "email",
                UniversityAverageScore = 6.5d,
                MathScore = 9.0d,
                MobilePhone = "+375(33)1922",
                Skype = "skype",
                StartDate = System.DateTime.Now,
                BirthDate = System.DateTime.Now,
                Address = "dom 5"
            };

            //Act
            repo.Create(user);

            //Assert
            Assert.IsNotNull(repo.GetById(user.UserId));
            Assert.AreEqual("skype", user.Skype);
        }

        [Test]
        public void ShouldDeleteUserProfileById()
        {
            //Arrange
            int id = 2;
            var repo = SeedUserProfileRepository();

            //Act
            repo.DeleteById(id);

            //Assert
            Assert.IsNull(repo.GetById(id));
        }

        [Test]
        public void ShoulGetUserProfileById()
        {
            //Arrange
            int id = 3;
            var repo = SeedUserProfileRepository();

            //Act
            repo.GetById(id);

            //Assert
            Assert.AreEqual("username3", repo.GetById(id).Name);
        }
    }
}
