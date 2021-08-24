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
    public class UpdatePassWordViewModel : ViewModelBase
    {
        //private List<Nhanvien> nhanviens = CRUD_Nhanvien.GetListWorker();
        private string _statusText = string.Empty;
        private string _colorText = string.Empty;
        private bool _IsUpdatePass = false;
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
                StatusText = "Thông tin chưa đầy đủ";
                ColorText = "red";              
            }
            else
            {
                if (RePassword == NewPassword)
                {
                    Login objc = new Login();
                    var properties = Application.Current.Properties;
                    if (OldPassword == (string)properties["Password"])
                    {
                        objc.Name = (string)properties["UserName"];
                        objc.Password = NewPassword;
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
                    StatusText = "Error!! Sai mat khau";
                    ColorText = "red";
                }
            }
        }
    }
}
