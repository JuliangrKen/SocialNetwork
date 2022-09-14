using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views
{
    public class UserAddFriendView
    {
        private FriendService friendService;

        public UserAddFriendView(FriendService friendService)
        {
            this.friendService = friendService;
        }

        public void Show(User user)
        {
            var friendAddingData = new FriendAddingData()
            {
                UserId = user.Id,
                FriendEmail = user.Email
            };

            Console.WriteLine("Введите почтовый адресс пользователя, которого вы хотите добавить в друзья: ");
            friendAddingData.FriendEmail = Console.ReadLine();

            try
            {
                friendService.AddFriend(friendAddingData);
            }
            catch (ArgumentNullException)
            {
                AlertMessage.Show("Введите корректное значение!");
            }
            catch (UserNotFoundException)
            {
                AlertMessage.Show("Не удалось найти пользователя!");
            }
            catch (UserIsAlreadyFriendException)
            {
                AlertMessage.Show("Пользователь уже находится у вас в друзьях!");
            }
        }
    }
}