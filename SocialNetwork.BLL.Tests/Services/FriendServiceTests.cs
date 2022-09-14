using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetwork.BLL.Models;

namespace SocialNetwork.BLL.Services.Tests
{
    [TestClass()]
    public class FriendServiceTests
    {
        [TestMethod()]
        public void AddFriendMustThrowArgumentNullExceptionWhenInvalidEmail()
        {
            var friendsService = new FriendService();

            var friendAddingData = new FriendAddingData()
            {
                FriendEmail = null,
            };

            Assert.ThrowsException<ArgumentNullException>(() => friendsService.AddFriend(friendAddingData));

            friendAddingData.FriendEmail = "notCorrectEmail";
            Assert.ThrowsException<ArgumentNullException>(() => friendsService.AddFriend(friendAddingData));
        }
    }
}