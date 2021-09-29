using BA_App.DataAPI;
using BA_App.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace BA_App.ViewModels
{
    public class UserInfoViewModel : ViewModelBase
    {
        private string _myname = string.Empty;
        private string _myid = string.Empty;
        private string _myimg = string.Empty;

        // public Nhanvien nhanvien;
        private List<Employees> WorkList = CRUD_Nhanvien.GetListWorker();
        public Command Signout { get; }
        public Command ChangePassWord { get; }
        public string Myname { 
            get { return _myname; }
            set { SetProperty(ref _myname, value); } 
            }
        public string Myid
        {
            get { return _myid; }
            set { SetProperty(ref _myid, value); }
        }
        public string Myimg
        {
            get { return _myimg; }
            set { SetProperty(ref _myimg, value); }
        }
        private readonly INavigationService _navigationService;
        public UserInfoViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Signout = new Command(SignoutClicked);
            ChangePassWord = new Command(ChangePassWordClicked);                     
            InfoUser();

        }
        public void InfoUser()
        {
            var properties = Application.Current.Properties;
            var query = from list in WorkList
                        where list.EmployeeId.Trim() == (string)properties["UsersID"]
                        select list;
            var userinfo = query.FirstOrDefault();
            Myname = userinfo.EmployeeName;
            Myid = userinfo.EmployeeId;
            Myimg = userinfo.EmployeeImage;
        }
        private async void ChangePassWordClicked(object obj)
        {
            //Chuyển đến trang tạo tài khoản khi cLick
            await _navigationService.NavigateAsync("UpdatePassWord");
        }
        private async void SignoutClicked(object obj)
        {
            //Chuyển đến trang tạo tài khoản khi cLick
            await _navigationService.NavigateAsync("/Login");
            //var pageIndex = Application.Current.MainPage.Navigation.NavigationStack.Count;
            //if (pageIndex == 2)
            //{
            //    Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - pageIndex]);
            //}
        }
    }
}
