using BA_App.DataAPI;
using BA_App.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BA_App.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _statusText = string.Empty;
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private bool _iSToggle = false;
        public static bool RemberToggle = false;

        //Binding dữ liệu 2 chiều StatusText
        public string StatusText
        {
            get { return _statusText; }
            set { SetProperty(ref _statusText, value); }
        }

        //Khai báo UserName kiểu binding dữ liệu 2 chiều
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        //Khai báo Password kiểu binding dữ liệu 2 chiều
        public string PassWord
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        //Khai báo ISToggle kiểu binding dữ liệu 2 chiều
        public bool ISToggle
        {
            get { return _iSToggle; }
            set { SetProperty(ref _iSToggle, value); }
        }
        public Login login;
        private readonly INavigationService _navigationService;
        public Command LoginCommand { get; }
        public Command CreateUserCommand { get; }
        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //Khai báo sự kiện khi click Login
            LoginCommand = new Command(OnLoginClicked);
            //Khai báo sự kiện khi click tạo tài khoản
            CreateUserCommand = new Command(CreateUserClicked);
            //khi Toggle là true thì lấy tài khoản đã lưa vào sesstion gán
            if (RemberToggle)
            {
                ISToggle = RemberToggle;
                var properties = Application.Current.Properties;
                if (properties.ContainsKey("UserName"))
                {
                    _userName = (string)properties["UserName"];
                    _password = (string)properties["Password"];
                }

            }
        }
        private async void OnLoginClicked(object obj)
        {
            login = new Login { Name = UserName, Password = PassWord };
            StatusText = "";
            //Lưa trạng thái ghi nhớ của Toggle
            RemberToggle = ISToggle;
            // IsBusy trạng thái chờ
            IsBusy = true;
            await Task.Delay(500);
            //IsLoginFalse trạng thái hiển thị đăng nhập
            //Kiểm tra thông tin đã điền chưa
            if (string.IsNullOrEmpty(PassWord) || string.IsNullOrEmpty(UserName))
            {
                StatusText = "Thông tin chưa chính xác";
                IsBusy = false;
            }
            else
            {
                var user = CRU_User.GetInfoUser(login);
                //Kiểm tra người dùng có tồn tại không
                if (user.success == false)
                {
                    IsBusy = false;
                    StatusText = "tài khoản hoặc mật khẩu không đúng";
                    return;
                }
                else
                {
                   // Lưa tài khoản Session trên Ram, nếu đăng nhập thành công
                    var properties = Application.Current.Properties;
                    if (!properties.ContainsKey("UserName") || !properties.ContainsKey("Password"))
                    {
                        properties.Add("UsersID", user.data.Manv);
                        properties.Add("UserName", UserName);
                        properties.Add("Password", PassWord);
                    }
                    else
                    {
                        properties["UsersID"] = user.data.Manv;
                        properties["UserName"] = UserName;
                        properties["Password"] = PassWord;
                    }
                   // đăng nhập thành công, chuyển đến trang chính
                    await _navigationService.NavigateAsync("TabbedMainPage");
                    var pageIndex = Application.Current.MainPage.Navigation.NavigationStack.Count;
                    if (pageIndex == 2)
                    {
                        Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex]);
                    }

                    IsBusy = false;
                }
            }
            //Lấy thông tin người dùng người dùng
            
        }
        private async void CreateUserClicked(object obj)
        {
            //Chuyển đến trang tạo tài khoản khi cLick
            await _navigationService.NavigateAsync("AddPassWord");
        }
    }
}
