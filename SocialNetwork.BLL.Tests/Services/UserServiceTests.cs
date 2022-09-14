using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;

namespace SocialNetwork.BLL.Services.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        [TestMethod()]
        public void RegisterMustThrowArgumentNullExceptionWhenInvalidFirstName()
        {
            var userService = new UserService();

            var userRegistrationData = new UserRegistrationData()
            {
                FirstName = null,
                LastName = "LastName",
                Password = "Password",
                Email = "Email@gmail.com",
            };

            Assert.ThrowsException<ArgumentNullException>(() => userService.Register(userRegistrationData));

            userRegistrationData.FirstName = "";
            Assert.ThrowsException<ArgumentNullException>(() => userService.Register(userRegistrationData));
        }

        [TestMethod()]
        public void RegisterMustThrowArgumentNullExceptionWhenInvalidLastName()
        {
            var userService = new UserService();

            var userRegistrationData = new UserRegistrationData()
            {
                FirstName = "LastName",
                LastName = null,
                Password = "Password",
                Email = "Email@gmail.com",
            };

            Assert.ThrowsException<ArgumentNullException>(() => userService.Register(userRegistrationData));

            userRegistrationData.LastName = "";
            Assert.ThrowsException<ArgumentNullException>(() => userService.Register(userRegistrationData));
        }
    }
}