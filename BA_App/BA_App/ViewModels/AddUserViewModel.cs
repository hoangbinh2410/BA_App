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
    public class AddUserViewModel : ViewModelBase
    {
        private bool _iname = false;
        private string _ername = string.Empty;
        private bool _iadress = false;
        private string _eradress = string.Empty;
        private bool _imanv = false;
        private string _erdepartment = string.Empty;
        private bool _idepartment = false;
        private string _ermanv = string.Empty;
        private bool _igender = false;
        private string _ergender = string.Empty;
        private string _statusText  = string.Empty;
        private string _color = string.Empty;
        // kiểm tra nhập tên
        public bool IName
        {
            get { return _iname; }
            set { SetProperty(ref _iname, value); }
        }
        public string ErName
        {
            get { return _ername; }
            set { SetProperty(ref _ername, value); }
        }
        //Kiểm tra nhập đia chỉ
        public bool IAdress
        {
            get { return _iadress; }
            set { SetProperty(ref _iadress, value); }
        }
        public string ErAdress
        {
            get { return _eradress; }
            set { SetProperty(ref _eradress, value); }
        }
        //Kiểm tra nhập mã nhân viên
        public bool IManv
        {
            get { return _imanv; }
            set { SetProperty(ref _imanv, value); }
        }
        public string ErManv
        {
            get { return _ermanv; }
            set { SetProperty(ref _ermanv, value); }
        }
        //Kiểm tra nhập phòng
        public bool IDepartment
        {
            get { return _idepartment; }
            set { SetProperty(ref _idepartment, value); }
        }
        public string ErDepartment
        {
            get { return _erdepartment; }
            set { SetProperty(ref _erdepartment, value); }
        }          
        //Kiểm tra giới tính
        public bool IGender
        {
            get { return _igender; }
            set { SetProperty(ref _igender, value); }
        }
        public string ErGender
        {
            get { return _ergender; }
            set { SetProperty(ref _ergender, value); }
        }
        //Binding dữ liệu 2 chiều StatusText
        public string StatusText
        {
            get { return _statusText; }
            set { SetProperty(ref _statusText, value); }
        }
        //Binding dữ liệu 2 chiều Color
        public string Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }
        public string Name { get; set; }
        public object Gender { get; set; }
        public object Department { get; set; }
        public string Manv { get; set; }
        public string Adress { get; set; }

        public DateTime BirthDay = DateTime.Now;
        public Command AddWorkerCommand { get; }
        private readonly INavigationService _navigationService;
        public AddUserViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddWorkerCommand = new Command(AddWorkerClicked);
            Title = "Thêm công nhân";

        }
        private async void AddWorkerClicked(object obj)
        {
            StatusText = "";
            IsBusy = true;
            await Task.Delay(1000);
            //kiểm tra thông tin đã điền đầy đúng chưa?
            if (string.IsNullOrEmpty(Name) || Department == null || Gender == null|| string.IsNullOrEmpty(Adress)|| string.IsNullOrEmpty(Manv))
            {
            
                if (string.IsNullOrEmpty(Name))
                {
                    IName = true;
                    ErName = "Nhập tên";
                }
                if (Department == null)
                {
                    IDepartment = true;
                    ErDepartment = "Chọn phòng";
                }
                if (string.IsNullOrEmpty(Adress))
                {
                    IAdress = true;
                    ErAdress = "Nhập địa chỉ";
                }
                if (string.IsNullOrEmpty(Manv))
                {
                    IManv = true;
                    ErManv = "Nhập mã nhân viên";
                }
                if (Gender == null)
                {
                    IGender = true;
                    ErGender = "Chọn giới tính";
                }
                StatusText = "Điền đầy đủ thông tin !!!";
                Color = "red";
                IsBusy = false;
                return;
            }
            //if (Gender != "Nam" && (Gender != "Nữ"))
            //{
            //    StatusText = "Thông tin chưa chính xác !!!";
            //    Color = "red";
            //    IsBusy = false;
            //    return;
            //}
            else
            {
                List<Departments> ListDepartment;
                ListDepartment = RDepartment.GetListDepartment();
                var qry = (from a in ListDepartment
                           where a.DepartmentName.Trim() == Department.ToString().Trim()
                           select a.DepartmentId).FirstOrDefault();
                Employees nhanvien = new Employees();
                nhanvien.EmployeeName = Name;
                nhanvien.EmployeeSex = Gender.ToString();
                nhanvien.EmployeeBirth = BirthDay;
                nhanvien.EmployeeDepartmentId = qry;
                nhanvien.EmployeeId = Manv;
                nhanvien.EmployeeAdress = Adress;
                try
                {
                    var boolResult = CRUD_Nhanvien.AddNewWorker(nhanvien);
                    if (boolResult == true)
                    {
                        StatusText = "Đã thêm thành công !!!";
                        Color = "blue";
                        MessagingCenter.Send<AddUserViewModel>(this, "Add");
                        await _navigationService.GoBackAsync();
                    }
                    else
                    {
                        StatusText = "Thất bại !!!";
                        Color = "red";
                    }
                }
                catch (Exception ex)
                {
                    //FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
                }
                IsBusy = false;
            }
        }
    }
}
