using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.Services
{
    public class MessageService
    {
        public const int MaxLengthMessage = 5000;
        private readonly IMessageRepository messageRepository;
        private readonly IUserRepository userRepository;

        public MessageService()
        {
            messageRepository = new MessageRepository();
            userRepository = new UserRepository();
        }

        public void SendMessage(MessageSendingData messageData)
        {
            if (string.IsNullOrEmpty(messageData.Content))
                throw new ArgumentNullException();

            if (messageData.Content.Length > MaxLengthMessage)
                throw new ArgumentNullException();

            if (string.IsNullOrEmpty(messageData.RecipientEmail))
                throw new ArgumentNullException();

            if (!new EmailAddressAttribute().IsValid(messageData.RecipientEmail))
                throw new ArgumentNullException();

            var recipient = userRepository.FindByEmail(messageData.RecipientEmail);
            if (recipient == null)
                throw new UserNotFoundException();

            var messageEntity = new MessageEntity()
            {
                sender_id = messageData.SenderId,
                recipient_id = recipient.id,
                content = messageData.Content,
            };

            if (messageRepository.Create(messageEntity) == 0)
                throw new Exception();
        }

        public IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId) =>
            GetMessages(messageRepository.FindByRecipientId(recipientId));

        public IEnumerable<Message> GetOutcomingMessagesByUserId(int senderId) =>
            GetMessages(messageRepository.FindBySenderId(senderId));

        private IEnumerable<Message> GetMessages(IEnumerable<MessageEntity> messageEntities)
        {
            var messages = new List<Message>();

            messageEntities.ToList().ForEach(m =>
            {
                var senderUserEntity = userRepository.FindById(m.sender_id);
                var recipientUserEntity = userRepository.FindById(m.recipient_id);

                messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
            });

            return messages;
        }
    }
}