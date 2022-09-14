using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;

namespace SocialNetwork.PLL.Views
{
    public class UserFriendsView
    {
        private FriendService friendService;

        public UserFriendsView(FriendService friendService)
        {
            this.friendService = friendService;
        }

        public void Show(User user)
        {
            var friendsList = friendService.GetFriendsByUserId(user.Id).OrderBy(x => x.FirstName).ToList();

            if (friendsList.Count == 0)
            {
                Console.WriteLine("У вас нет друзей :(");
                return;
            }

            for (int i = 0; i < friendsList.Count; i++)
                Console.WriteLine($"{i + 1}. {friendsList[i].FirstName} {friendsList[i].LastName}, email: {friendsList[i].Email};\n");
        }
    }
}