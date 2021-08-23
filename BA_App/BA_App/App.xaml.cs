using BA_App.ViewModels;
using BA_App.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace BA_App
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/UpdatePassWord");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Login, LoginViewModel>();
            containerRegistry.RegisterForNavigation<AddPassWord, AddPassWordViewModel>();
            containerRegistry.RegisterForNavigation<TabbedMainPage, TabbedMainPageViewModel>();
            containerRegistry.RegisterForNavigation<ListViewUser, ListViewUserViewModel>();
            containerRegistry.RegisterForNavigation<AddUser, AddUserViewModel>();
            containerRegistry.RegisterForNavigation<UserInfo, UserInfoViewModel>();
            containerRegistry.RegisterForNavigation<DeleteUser, DeleteUserViewModel>();
            containerRegistry.RegisterForNavigation<UpdateUser, UpdateUserViewModel>();
            containerRegistry.RegisterForNavigation<UpdatePassWord, UpdatePassWordViewModel>();
        }
    }
}
