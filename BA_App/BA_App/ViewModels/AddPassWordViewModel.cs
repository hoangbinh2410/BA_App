using BA_App.DataAPI;
using BA_App.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BA_App.ViewModels
{
    public class AddPassWordViewModel : ViewModelBase
    {
        private string _reerpass = string.Empty;
        private string _ermanv = string.Empty;
        private string _ername = string.Empty;
        private bool _name = false;
        private bool _manv = false;
        private string _erpass = string.Empty;
        private bool _pass = false;
        private bool _repass = false;        
        private string _statusText = string.Empty;
        private string _colorText = string.Empty;
        private bool _iSCreateUser = false;
        //Kiểm tra mật khẩu
        public bool Pass
        {
            get { return _pass; }
            set { SetProperty(ref _pass, value); }

        }
        public string ErPass
        {
            get { return _erpass; }
            set { SetProperty(ref _erpass, value); }
        }
        //Kiểm tra nhập lại mật khẩu
        public bool RePass
        {
            get { return _repass; }
            set { SetProperty(ref _repass, value); }

        }
        public string ReErPass
        {
            get { return _reerpass; }
            set { SetProperty(ref _reerpass, value); }
        }
        //Kiểm tra nhập tài khoản
        public bool Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string ErName
        {
            get { return _ername; }
            set { SetProperty(ref _ername, value); }
        }
        // Kiểm tra nhập mã nhân viên
        public bool Manv
        {
            get { return _manv; }
            set { SetProperty(ref _manv, value); }
        }
        public string ErManv
        {
            get { return _ermanv; }
            set { SetProperty(ref _ermanv, value); }
        }
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
            if (string.IsNullOrEmpty(CreateUserName) || string.IsNullOrEmpty(CreatePassword) || string.IsNullOrEmpty(CreateManv) || string.IsNullOrEmpty(ISCreatePassword))
            {
                IsBusy = false;
                StatusText = "Thông tin chưa đầy đủ";
                ColorText = "red";
                if (string.IsNullOrEmpty(CreateUserName))
                {
                    Name = true;
                    ErName = "Nhập tài khoản";
                }
                if (string.IsNullOrEmpty(CreatePassword))
                {
                    Pass = true;
                    ErPass = "Nhập mật khẩu";
                }
                if (string.IsNullOrEmpty(CreateManv))
                {
                    Manv = true;
                    ErManv = "Nhập  mã nhân viên";
                }
                if (string.IsNullOrEmpty(ISCreatePassword))
                {
                    RePass = true;
                    ReErPass = "Nhập  mã nhân viên";
                }

            }
            else
            {
                if (CreatePassword == ISCreatePassword)
                {
                    MD5 mh = MD5.Create();
                    //Chuyển kiểu chuổi thành kiểu byte
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes($"{CreatePassword}");
                    //mã hóa chuỗi đã chuyển
                    byte[] hash = mh.ComputeHash(inputBytes);
                    //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
                    StringBuilder MD5Pass = new StringBuilder();

                    for (int i = 0; i < hash.Length; i++)
                    {
                        MD5Pass.Append(hash[i].ToString("X2"));
                    }
                    User objc = new User();
                    objc.UserName = CreateUserName;
                    objc.UserPass = MD5Pass.ToString();
                    objc.UserId = CreateManv;
                    try
                    {
                        //Gửi dữ liệu đi tạo tải khoản mới
                        var Rerult = CRU_User.CreateUser(objc);
                        if (Rerult == true)
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
                            StatusText = "Lỗi server!";
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
                    RePass = true;
                    Pass = true;
                    ErPass = "Chưa chính xác";
                    ReErPass = "Chưa chính xác";
                    StatusText = "Mật khẩu không trùng khớp !!!";
                    ColorText = "red";
                }
                IsBusy = false;
            }
        }
    }
}
