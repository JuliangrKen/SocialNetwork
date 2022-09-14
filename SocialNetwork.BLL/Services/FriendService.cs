using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.Services
{
    public class FriendService
    {
        private readonly IUserRepository userRepository;
        private readonly IFriendRepository friendRepository;

        public FriendService()
        {
            userRepository = new UserRepository();
            friendRepository = new FriendRepository();
        }

        public void AddFriend(FriendAddingData friendAddingData)
        {
            if (string.IsNullOrEmpty(friendAddingData.FriendEmail))
                throw new ArgumentNullException();

            if (!new EmailAddressAttribute().IsValid(friendAddingData.FriendEmail))
                throw new ArgumentNullException();

            var friendUserEntity = userRepository.FindByEmail(friendAddingData.FriendEmail);
            if (friendUserEntity == null)
                throw new UserNotFoundException();

            if (friendUserEntity.id == friendAddingData.UserId)
                throw new ArgumentNullException();

            var userFriends = GetFriendsByUserId(friendAddingData.UserId);
            if (!userFriends.ToList().TrueForAll(x => x.Email != friendUserEntity.email))
                throw new UserIsAlreadyFriendException();

            var userFE = new FriendEntity()
            {
                user_id = friendAddingData.UserId,
                friend_id = friendUserEntity.id
            };

            var friendFE = new FriendEntity()
            {
                user_id = friendUserEntity.id,
                friend_id = friendAddingData.UserId
            };

            if (friendRepository.Create(userFE) == 0)
                throw new Exception();

            if (friendRepository.Create(friendFE) == 0)
                throw new Exception();
        }

        public IEnumerable<Friend> GetFriendsByUserId(int id) =>
            GetFriends(friendRepository.FindAllByUserId(id));

        private IEnumerable<Friend> GetFriends(IEnumerable<FriendEntity> friendEntities)
        {
            var friends = new List<Friend>();

            friendEntities.ToList().ForEach(m =>
            {
                var friend = userRepository.FindById(m.friend_id);

                friends.Add(new Friend(friend.id, friend.firstname, friend.lastname, friend.email));
            });

            return friends;
        }
    }
}