using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System.Text;

namespace SocialNetwork.BLL.Tests.Services
{
    [TestClass()]
    public class MessageServiceTests
    {
        [TestMethod()]
        public void SendMessageMustThrowArgumentNullExceptionWhenInvalidContext()
        {
            var messageService = new MessageService();

            var message = new MessageSendingData()
            {
                Content = "",
                SenderId = 0,
                RecipientEmail = "dev@mail.ru"
            };

            Assert.ThrowsException<ArgumentNullException>(() => messageService.SendMessage(message));

            var strBuilder = new StringBuilder();

            for (int i = 1; i <= 5001; i++)
                strBuilder.Append(i);

            message.Content = strBuilder.ToString();
            Assert.ThrowsException<ArgumentNullException>(() => messageService.SendMessage(message));
        }

        [TestMethod()]
        public void SendMessageMustThrowArgumentNullExceptionWhenInvalidEmail()
        {
            var messageService = new MessageService();

            var messageSendingData = new MessageSendingData()
            {
                RecipientEmail = null
            };

            Assert.ThrowsException<ArgumentNullException>(() => messageService.SendMessage(messageSendingData));
            
            messageSendingData.RecipientEmail = "notCorrectEmail";
            Assert.ThrowsException<ArgumentNullException>(() => messageService.SendMessage(messageSendingData));
        }
    }
}