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
    public class AddPassWordViewModel : ViewModelBase
    {
        private string _statusText = string.Empty;
        private string _colorText = string.Empty;
        private bool _iSCreateUser = false;
        // Khai báo StatusText binding 2 chiều
        public string StatusText
        {
            get { return _statusText; }
            set { SetProperty(ref _statusText, value); }
        }

        // Khai báo ColorText binding 2 chiều
        public string ColorText
        {
            get { return _colorText; }
            set { SetProperty(ref _colorText, value); }
        }

        // Khai báo ISCreateUser binding 2 chiều
        public bool ISCreateUser
        {
            get { return _iSCreateUser; }
            set { SetProperty(ref _iSCreateUser, value); }
        }

        public string CreateUserName { get; set; }
        public string CreatePassword { get; set; }
        public string ISCreatePassword { get; set; }
        public string CreateManv { get; set; }


        //Khai báo sự kiện khi CreateUser
        public Command CreateUserCommand { get; }
        private readonly INavigationService _navigationService;
        public AddPassWordViewModel(INavigationService navigationService)
        {
            CreateUserCommand = new Command(CreateUserClicked);
            _navigationService = navigationService;

        }
        private async void CreateUserClicked(object obj)
        {
            //các trạng thái biểu diễn
            StatusText = "";
            ISCreateUser = true;
            IsBusy = true;
           
            await Task.Delay(1000);
            //Kiểm tra xem các thông tin có null hoặc rỗng không
            if (string.IsNullOrEmpty(CreateUserName) || string.IsNullOrEmpty(CreatePassword) || string.IsNullOrEmpty(CreateManv))
            {
                IsBusy = false;
                StatusText = "Thông tin chưa đầy đủ";
                ColorText = "red";
                return;
            }


            if (CreatePassword == ISCreatePassword)
            {
                User objc = new User();
                objc.Name = CreateUserName;
                objc.Pass = CreatePassword;
                objc.Manv = CreateManv;
                try
                {
                    //Gửi dữ liệu đi tạo tải khoản mới
                    var Rerult = CRU_User.CreateUser(objc);
                    if (Rerult == "Thành công !!!")
                    {
                        //StatusText = Rerult;
                        //ColorText = "blue";
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Đăng ký thành công!!!", "ok");
                        //Đăng ký thành công, chuyển về trang Login, và remote trang tạo tài khoản
                        await _navigationService.GoBackAsync();
                        //gửi tên tài khoản mới tạo đến trang Login
                        MessagingCenter.Send<AddPassWordViewModel, string>(this, "CreateUser", CreateUserName);
                    }
                    else
                    {
                        StatusText = Rerult;
                        ColorText = "red";
                    }
                }
                catch (Exception ex)
                {
                    //FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
                }
            }
            else
            {
                StatusText = "Mật khẩu không trùng khớp !!!";
                ColorText = "red";
            }
            IsBusy = false;
        }
    }
}
