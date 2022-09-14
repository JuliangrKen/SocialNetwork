using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Views;

namespace SocialNetwork
{
    class Program
    {
        private static MessageService? messageService;
        private static UserService? userService;
        private static FriendService? friendService;
        public static MainView? mainView;
        public static RegistrationView? registrationView;
        public static AuthenticationView? authenticationView;
        public static UserMenuView? userMenuView;
        public static UserInfoView? userInfoView;
        public static UserDataUpdateView? userDataUpdateView;
        public static MessageSendingView? messageSendingView;
        public static UserIncomingMessageView? userIncomingMessageView;
        public static UserOutcomingMessageView? userOutcomingMessageView;
        public static UserAddFriendView? userAddFriendsView;
        public static UserFriendsView? userFriendsView;

        static void Main(string[] args)
        {
            userService = new UserService();
            messageService = new MessageService();
            friendService = new FriendService();

            mainView = new MainView();
            registrationView = new RegistrationView(userService);
            authenticationView = new AuthenticationView(userService);
            userMenuView = new UserMenuView(userService);
            userInfoView = new UserInfoView();
            userDataUpdateView = new UserDataUpdateView(userService);
            messageSendingView = new MessageSendingView(messageService, userService);
            userIncomingMessageView = new UserIncomingMessageView();
            userOutcomingMessageView = new UserOutcomingMessageView();
            userAddFriendsView = new UserAddFriendView(friendService);
            userFriendsView = new UserFriendsView(friendService);

            while (true)
            {
                mainView.Show();
            }
        }
    }
}