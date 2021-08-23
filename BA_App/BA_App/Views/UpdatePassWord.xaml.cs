using BA_App.DataAPI;
using BA_App.Model;
using Prism.Navigation;
using System;
using Xamarin.Forms;

namespace BA_App.Views
{
    public partial class UpdatePassWord : ContentPage
    {
        //private INavigationService _navigationService;
        //private User _objUser { get; set; }
        public UpdatePassWord()
        {
            //MessagingCenter.Subscribe<UpdatePassWord, User>(this, "ChangePW", (sender, objUser) =>
            //{
            //    _objUser = objUser;
            //});
            //MessagingCenter.Subscribe<UpdatePassWord, INavigationService>(this, "ChangePW", (sender, navigationService) =>
            //{
            //    _navigationService = navigationService;
            //});
            InitializeComponent();
        }

        //private async void Clicked_ChangePassWord(object sender, System.EventArgs e)
        //{
        //    //Kiểm tra thông tin đã nhập đầy đủ chưa
        //    if (string.IsNullOrEmpty(password.Text) || string.IsNullOrEmpty(newPassword.Text) || string.IsNullOrEmpty(iSPassword.Text))
        //    {
        //        await DisplayAlert("Thông báo", "Thông tin chưa đúng", "ok");
        //        return;
        //    }
        //    // Nếu mật khẩu mới không trùng với mật khẩu cũ thì return;
        //    bool isUsernamePasswordValid = false;
        //    //ScryptEncoder encoder = new ScryptEncoder();
        //   // isUsernamePasswordValid = encoder.Compare(password.Text, _objUser.PasswordHash);
        //    if (!isUsernamePasswordValid)
        //    {
        //        await DisplayAlert("Thông báo", "Mật khẩu không đúng", "ok");
        //        return;
        //    }
        //    // Nếu mật khẩu nhập 2  lần sai thì return;
        //    if (newPassword.Text != iSPassword.Text)
        //    {
        //        await DisplayAlert("Thông báo", "Mật khẩu không trùng khớp", "ok");
        //        return;
        //    }
        //    try
        //    {
        //        // UserList.Where(usr => usr.UserName == _objUser.UserName)
        //        //.Select(usr => { usr.Password = newPassword.Text; return usr; })
        //        //.ToList();
        //        User user = new User();
        //        user.Name = _objUser.Name;
             
        //        user.Pass = newPassword.Text;
        //        user.Manv = _objUser.Manv;
        //        //Gửi dữ liệu đi update
        //       // bool boolChangePw = CRU_User.UpdateUser(user);
        //        //if (boolChangePw)
        //        //{
        //        //    //Thông báo  thành công và quay trở về trang đăng nhập
        //        //    //Xóa toàn bộ trang đã có
        //        //    await DisplayAlert("Thông báo", "Thành công!!!", "ok");
        //        //    await _navigationService.NavigateAsync("LoginPage");
        //        //    var pageIndex = Application.Current.MainPage.Navigation.NavigationStack.Count;
        //        //    if (pageIndex == 3)
        //        //    {
        //        //        Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex]);
        //        //        Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex + 1]);
        //        //    }
        //        //    if (pageIndex == 4)
        //        //    {
        //        //        Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex]);
        //        //        Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex + 1]);
        //        //        Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex + 2]);
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    await DisplayAlert("Thông báo", "Thất bại,Server gặp lỗi !!!", "ok");
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //       // FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
        //    }

        //}

        //private void Clicked_CancelChangePW(object sender, System.EventArgs e)
        //{

        //}
    }
}
