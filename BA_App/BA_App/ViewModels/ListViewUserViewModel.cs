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
        private List<Employees> employees = CRUD_Nhanvien.GetListWorker();
        private List<DataEmployee> nhanviens = GetDataEmployees();
        private List<Departments> _departments = RDepartment.GetListDepartment();
        private List<DataEmployee> _nvRemember;        
        private string _departmentFilter = string.Empty;
        private static List<DataEmployee> GetDataEmployees()
        {
            List<Departments> _departments = RDepartment.GetListDepartment();
            List<Employees> employees = CRUD_Nhanvien.GetListWorker();
            List<DataEmployee> nhanviens = new List<DataEmployee>();
            var query = from a in employees
                        join
                    b in _departments
                    on a.EmployeeDepartmentId equals b.DepartmentId
                        select new
                        {
                            ID = a.ID,
                            Name = a.EmployeeName,
                            Date = a.EmployeeBirth,
                            Adress = a.EmployeeAdress,
                            Department = b.DepartmentName,
                            Sex = a.EmployeeSex,
                            IDName = a.EmployeeId,                            
                        };

            query.ToList().ForEach(a => {

                var nhanVien = new DataEmployee();
                nhanVien.ID = a.ID;
                nhanVien.EmployeeId = a.IDName;
                nhanVien.EmployeeAdress = a.Adress;
                nhanVien.EmployeeBirth = a.Date;
                nhanVien.EmployeeDepartmentName = a.Department;
                nhanVien.EmployeeSex = a.Sex;
                nhanVien.EmployeeName = a.Name;
                nhanviens.Add(nhanVien);
            });
            return nhanviens;
        }
        public List<DataEmployee> WorkList
        {
            get { return nhanviens; }
            set { SetProperty(ref nhanviens, value); }
        }
        public List<Departments> departments
        {
            get { return _departments; }
            set { SetProperty(ref _departments, value); }
        }
        public Command<DataEmployee> DeleteWorkerCommand { get; }
        // Khai báo UpdateWorkerCommand binding sự kiện
        public Command<DataEmployee> UpdateWorkerCommand { get; }
        // Khai báo AddWorkerCommand binding thêm mới công nhân
        public Command AddWorkerCommand { get; }
        private readonly INavigationService _navigationService;
        public ListViewUserViewModel(INavigationService navigationService)
        {
           
            _nvRemember = WorkList;
            //var demo = CRUD_Nhanvien.GetListWorker();
            _navigationService = navigationService;
            DeleteWorkerCommand = new Command<DataEmployee>(DeleteWorkerClicked);
            UpdateWorkerCommand = new Command<DataEmployee>(UpdateWorkerClicked);
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

                WorkList = _nvRemember.Where(x => x.EmployeeDepartmentName == arg)?.ToList();
            });
            //Load lại Grid khi Filter theo tên tìm kiếm 
            MessagingCenter.Subscribe<ListViewUser, string>(this, "FilterByName", (sender, arg) =>
            {
                if (_departmentFilter == EnumPicKerFilter.TatCa.GetEnumDescription() || _departmentFilter == string.Empty)
                {
                    WorkList = _nvRemember.Where(x => x.EmployeeName.GetVnStringOnlyCharactersAndNumbers().ToUpper().Contains(arg.Trim().GetVnStringOnlyCharactersAndNumbers().ToUpper()))?.ToList();
                    return;
                }
                WorkList = _nvRemember.Where(x => x.EmployeeName.GetVnStringOnlyCharactersAndNumbers().ToUpper().Contains(arg.Trim().GetVnStringOnlyCharactersAndNumbers().ToUpper()) && x.EmployeeDepartmentName == _departmentFilter)?.ToList();
            });
            //Load Grid khi update nhân viên
            MessagingCenter.Subscribe<UpdateUserViewModel>(this, "Update", (sender) =>
            {
                WorkList = GetDataEmployees();
                _nvRemember = GetDataEmployees();
            });
            //Load Grid khi thêm mới nhân viên
            MessagingCenter.Subscribe<AddUserViewModel>(this, "Add", (sender) =>
            {
               WorkList = GetDataEmployees();
                _nvRemember = GetDataEmployees();
            });

        }
        //Xóa nhân viên
        private async void DeleteWorkerClicked(DataEmployee nhanvien)
        {
            List<DataEmployee> list;
            list = GetDataEmployees();
            var query = (from a in list
                         where a.EmployeeId.Trim() == nhanvien.EmployeeId.Trim()
                         select a.ID).FirstOrDefault();
            bool answer = await Application.Current.MainPage.DisplayAlert("Bạn muốn xóa?",nhanvien.EmployeeName, "Có", "Không");
            Debug.WriteLine("Answer: " + answer);
            if (answer)
            {
                var boolresult = CRUD_Nhanvien.DeleteWorker(query);
                WorkList = GetDataEmployees();
            }
            return;
        }
        /// <summary>Updates the worker clicked.
        /// menthod sử lý sự kiện khi vuốt kéo sang update nhân viên
        /// </summary>             
        /// <param name="objWorker">The object worker.</param>
        private async void UpdateWorkerClicked(DataEmployee objWorker)
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
