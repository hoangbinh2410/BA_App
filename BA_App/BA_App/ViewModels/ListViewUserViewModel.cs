using BA_App.DataAPI;
using BA_App.Enum;
using BA_App.Extentions;
using BA_App.Model;
using BA_App.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace BA_App.ViewModels
{
    public class ListViewUserViewModel : ViewModelBase
    {
        private List<Nhanvien> nhanviens = CRUD_Nhanvien.GetListWorker();
        private List<Nhanvien> _nvRemember = CRUD_Nhanvien.GetListWorker();
        private string _departmentFilter = string.Empty;
        public List<Nhanvien> WorkList
        {
            get { return nhanviens; }
            set { SetProperty(ref nhanviens, value); }
        }
        public Command<Nhanvien> DeleteWorkerCommand { get; }
        // Khai báo UpdateWorkerCommand binding sự kiện
        public Command<Nhanvien> UpdateWorkerCommand { get; }
        // Khai báo AddWorkerCommand binding thêm mới công nhân
        public Command AddWorkerCommand { get; }
        private readonly INavigationService _navigationService;
        public ListViewUserViewModel(INavigationService navigationService)
        {
            //var demo = CRUD_Nhanvien.GetListWorker();
            _navigationService = navigationService;
            DeleteWorkerCommand = new Command<Nhanvien>(DeleteWorkerClicked);
            UpdateWorkerCommand = new Command<Nhanvien>(UpdateWorkerClicked);
            AddWorkerCommand = new Command(AddWorkerClicked);
           // WorkList = CRUD_Nhanvien.GetListWorker();
            MessagingCenter.Subscribe<ListViewUser, string>(this, "FilterByDepartment", (sender, arg) =>
            {
                _departmentFilter = arg;
                if (arg == EnumPicKerFilter.TatCa.GetEnumDescription())
                {
                   
                    WorkList = _nvRemember;
                    return;
                }
                WorkList = _nvRemember.Where(x => x.Phong == arg)?.ToList();
            });
            //Load lại Grid khi Filter theo tên tìm kiếm 
            MessagingCenter.Subscribe<ListViewUser, string>(this, "FilterByName", (sender, arg) =>
            {
                if (_departmentFilter == EnumPicKerFilter.TatCa.GetEnumDescription() || _departmentFilter == string.Empty)
                {
                    WorkList = _nvRemember.Where(x => x.Ten.GetVnStringOnlyCharactersAndNumbers().ToUpper().Contains(arg.Trim().GetVnStringOnlyCharactersAndNumbers().ToUpper()))?.ToList();
                    return;
                }
                WorkList = _nvRemember.Where(x => x.Ten.GetVnStringOnlyCharactersAndNumbers().ToUpper().Contains(arg.Trim().GetVnStringOnlyCharactersAndNumbers().ToUpper()) && x.Phong == _departmentFilter)?.ToList();
            });
            //Load Grid khi update Worker
            MessagingCenter.Subscribe<UpdateUserViewModel>(this, "Update", (sender) =>
            {
                WorkList = CRUD_Nhanvien.GetListWorker();
                _nvRemember = CRUD_Nhanvien.GetListWorker();
            });
            //Load Grid khi thêm mới Worker
            MessagingCenter.Subscribe<AddUserViewModel>(this, "Add", (sender) =>
            {
                WorkList = CRUD_Nhanvien.GetListWorker();
                _nvRemember = CRUD_Nhanvien.GetListWorker();
            });

        }
        //Xóa nhân viên
        private async void DeleteWorkerClicked(Nhanvien nhanvien)
        {
            List<Nhanvien> list;
            list = CRUD_Nhanvien.GetListWorker();
            var query = (from a in list
                         where a.Manv.Trim() == nhanvien.Manv.Trim()
                         select a.ID).FirstOrDefault();
            bool answer = await Application.Current.MainPage.DisplayAlert("Bạn muốn xóa?",nhanvien.Ten, "Có", "Không");
            Debug.WriteLine("Answer: " + answer);
            if (answer)
            {
                var boolresult = CRUD_Nhanvien.DeleteWorker(query);
                list = CRUD_Nhanvien.GetListWorker();
            }
            return;
        }
        /// <summary>Updates the worker clicked.
        /// menthod sử lý sự kiện khi vuốt kéo sang update nhân viên
        /// </summary>
        /// <param name="objWorker">The object worker.</param>
        private async void UpdateWorkerClicked(Nhanvien objWorker)
        {
            //Chuyển đến trang WorkerUpdate
            var param = new NavigationParameters();
            param.Add("Nhanvien", objWorker);
            await _navigationService.NavigateAsync("UpdateUser", param);
        }
        private async void AddWorkerClicked()
        {
            //Chuyển đến trang WorkerAdd
            await _navigationService.NavigateAsync("AddUser");
        }
    }
}
