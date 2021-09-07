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
    public class UpdatePassWordViewModel : ViewModelBase
    {
        //private List<Nhanvien> nhanviens = CRUD_Nhanvien.GetListWorker();
        private bool _pass = false;
        private bool _renewpass = false;
        private bool _newpass = false;
        private string _Erpass = string.Empty;
        private string _Ernewpass = string.Empty;
        private string _Errenewpass = string.Empty;
        private string _statusText = string.Empty;
        private string _colorText = string.Empty;
        private bool _IsUpdatePass = false;
        // Khai báo StatusText binding 2 chiều
        public bool Pass
        {
            get { return _pass; }
            set { SetProperty(ref _pass, value); }
        }
        public string ErPass
        {
            get { return _Erpass; }
            set { SetProperty(ref _Erpass, value); }
        }
        public string ErnewPass
        {
            get { return _Ernewpass; }
            set { SetProperty(ref _Ernewpass, value); }
        }
        public bool newPass
        {
            get { return _newpass; }
            set { SetProperty(ref _newpass, value); }
        }
        public bool RenewPass
        {
            get { return _renewpass; }
            set { SetProperty(ref _renewpass, value); }
        }
        public string ErRenewPass
        {
            get { return _Errenewpass; }
            set { SetProperty(ref _Errenewpass, value); }
        }
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
        public bool IsUpdatePass
        {
            get { return _IsUpdatePass; }
            set { SetProperty(ref _IsUpdatePass, value); }
        }

        public string OldPassword { get; set; }
        public string RePassword { get; set; }
        public string NewPassword { get; set; }
        public Command UpdatePassCommand { get; }
        private User _objUser { get; set; }
        private readonly INavigationService _navigationService;
        public UpdatePassWordViewModel(INavigationService navigationService)
        {
            UpdatePassCommand = new Command(UpdatePassClicked);
            _navigationService = navigationService;
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _objUser = parameters.GetValue<User>("User");
            MessagingCenter.Send<UpdatePassWordViewModel, User>(this, "ChangePW", _objUser);
            MessagingCenter.Send<UpdatePassWordViewModel, INavigationService>(this, "ChangePW", _navigationService);
        }
        private async void UpdatePassClicked(object obj)
        {
            //các trạng thái biểu diễn
            StatusText = "";
            IsUpdatePass = true;
            IsBusy = true;
            await Task.Delay(500);
            if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(RePassword) || string.IsNullOrEmpty(NewPassword))
            {
                IsBusy = false;
                StatusText = "Kiểm tra thông tin";
                ColorText = "red";
                if (string.IsNullOrEmpty(OldPassword))
                {
                    Pass = true;
                    ErPass = "Chưa điền mật khẩu";
                }
                if (string.IsNullOrEmpty(RePassword))
                {
                    newPass = true;
                    ErnewPass = "Chưa điền mật khẩu";
                }
                if (string.IsNullOrEmpty(NewPassword))
                {
                    RenewPass = true;
                    ErRenewPass = "Chưa điền mật khẩu";
                }

            }
            else
            {
                if (RePassword == NewPassword)
                {
                    //ScryptEncoder encoder = new ScryptEncoder();
                    // mã hóa MD5
                    MD5 mh = MD5.Create();
                    //Chuyển kiểu chuổi thành kiểu byte
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes($"{NewPassword}");
                    //mã hóa chuỗi đã chuyển
                    byte[] hash = mh.ComputeHash(inputBytes);
                    //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
                    StringBuilder MD5NewPass = new StringBuilder();

                    for (int i = 0; i < hash.Length; i++)
                    {
                        MD5NewPass.Append(hash[i].ToString("X2"));
                    }
                    MD5 mh1 = MD5.Create();
                    //Chuyển kiểu chuổi thành kiểu byte
                    byte[] inputBytes1 = System.Text.Encoding.ASCII.GetBytes($"{OldPassword}");
                    //mã hóa chuỗi đã chuyển
                    byte[] hash1 = mh1.ComputeHash(inputBytes);
                    //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
                    StringBuilder MD5OldPass = new StringBuilder();

                    for (int i = 0; i < hash1.Length; i++)
                    {
                        MD5NewPass.Append(hash1[i].ToString("X2"));
                    }
                    Login objc = new Login();
                    var properties = Application.Current.Properties;
                    if (OldPassword == (string)properties["Password"])
                    {
                        objc.Name = (string)properties["UserName"];
                        objc.Password = MD5NewPass.ToString();
                        try
                        {
                            //Gửi dữ liệu doi mk
                            var Rerult = CRU_User.ChangeUser(objc);
                            if (Rerult.success == true)
                            {
                                StatusText = "Doi mk thanh cong";
                                ColorText = "blue";
                                await Application.Current.MainPage.DisplayAlert("Thông báo", "Doi mk thành công!!!", "ok");
                                //Đăng ký thành công, chuyển về trang Login, và remote trang tạo tài khoản
                                await _navigationService.NavigateAsync("Login");
                                //gửi tên tài khoản mới tạo đến trang Login
                                var pageIndex = Application.Current.MainPage.Navigation.NavigationStack.Count;
                                if (pageIndex == 3)
                                {
                                    Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex]);
                                    Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex + 1]);
                                }
                                if (pageIndex == 4)
                                {
                                    Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex]);
                                    Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex + 1]);
                                    Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex + 2]);
                                }

                            }
                            else
                            {
                                StatusText = "Error!! Sai mat khau";
                                ColorText = "red";
                            }
                        }
                        catch (Exception ex)
                        {
                            //FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
                        }

                    }
                }
                else
                {
                    RenewPass = true;
                    newPass = true;
                    StatusText = "Mật khẩu chưa trùng khớp";
                    ErnewPass = "Kiểm tra lại thông tin";
                    ErRenewPass = "Kiểm tra lại thông tin";
                    ColorText = "red";
                }
            }
        }
    }
}
