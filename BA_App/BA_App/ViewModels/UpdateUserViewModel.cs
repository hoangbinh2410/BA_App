using BA_App.DataAPI;
using BA_App.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Syncfusion.XForms.TextInputLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BA_App.ViewModels
{
    public class UpdateUserViewModel : ViewModelBase
    {
        private bool _iname = false;
        private string _ername = string.Empty;
        private bool _iadress = false;
        private string _eradress = string.Empty;
        private bool _imanv = false;
        private string _erdepartment = string.Empty;
        private bool _idepartment = false;
        private string _ermanv = string.Empty;
        private bool _ibirthday = false;
        private bool _igender = false;
        private string _ergender = string.Empty;
        private string _erbirthday = string.Empty;

        private string _statusText = string.Empty;
        private string _color = string.Empty;
        private string _name = string.Empty;
        private string _manv = string.Empty;
        private string _adress = string.Empty;
        private Departments _department = new Departments();
        private DateTime _birthDay = DateTime.Now;
        private object _gender ="";

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
        //Kiểm tra ngày sinh
        public bool IBirthDay
        {
            get { return _ibirthday; }
            set { SetProperty(ref _ibirthday, value); }
        }
        public string ErBirthDay
        {
            get { return _erbirthday; }
            set { SetProperty(ref _erbirthday, value); }
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
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        //Binding dữ liệu 2 chiều giới tính
        public object Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }
        //Binding dữ liệu 2 chiều Old
        public string Manv
        {
            get { return _manv; }
            set { SetProperty(ref _manv, value); }
        }
        //Binding dữ liệu 2 chiều Unit
        public Departments Department
        {
            get { return _department; }
            set { SetProperty(ref _department, value); }
        }
        public string Adress
        {
            get { return _adress; }
            set { SetProperty(ref _adress, value); }
        }
        //Binding dữ liệu 2 chiều
        public DateTime BirthDay
        {
            get { return _birthDay; }
            set { SetProperty(ref _birthDay, value); }
        }
        public Command UpdateCommand { get; }
        //public Command DeleteCommand { get; }
        private DataEmployee _objWorker { get; set; }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _objWorker = parameters.GetValue<DataEmployee>("Nhanvien");
           // _nameDepartment 
            //Binding dữ liệu khi chuyển trang
            Name = _objWorker.EmployeeName;
            Gender = _objWorker.EmployeeSex;
            Manv = _objWorker.EmployeeId;
            Department.DepartmentName = _objWorker.EmployeeDepartmentName;
            BirthDay = _objWorker.EmployeeBirth;
            Adress = _objWorker.EmployeeAdress;
        }
        public UpdateUserViewModel()
        {
            UpdateCommand = new Command(UpdateClicked);
            Title = "Sửa thông tin";
            Department.DepartmentName = "QA";
            StatusText = "";
            // Department.DepartmentName = "Phòng ban";
        }
        private async void UpdateClicked(object obj)
        {
            StatusText = "";
            //IsBusy chờ xử lý
            IsBusy = true;
            await Task.Delay(1000);
            //Dùng linq update thông tin khi thay đổi
            try
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Manv) || Department == null || string.IsNullOrEmpty(Adress) || Gender == null)
                {
                    StatusText = "Điền đầy đủ thông tin !!!";
                    Color = "red";
                    IsBusy = false;
                    if (string.IsNullOrEmpty(Name))
                    {
                        IName = true;
                        ErName = "Nhập tên";
                    }
                    if (string.IsNullOrEmpty(Manv))
                    {
                        IManv = true;
                        ErManv = "Nhập mã nhân viên";
                    }
                    if (Department == null)
                    {
                        IDepartment = true;
                        ErDepartment = "Chọn phòng";
                    }
                    if (Gender == null)
                    {
                        IGender = true;
                        ErGender = "Chọn giới tính";
                    }
                    if (string.IsNullOrEmpty(Adress))
                    {
                        IAdress = true;
                        ErAdress = "Nhập địa chỉ";
                    }

                    return;
                }
                //if ((string)Gender != "Nam" && ((string)Gender != "Nữ"))
                //{
                //    StatusText = "Thông tin chưa chính xác !!!";
                //    Color = "red";
                //    IsBusy = false;
                //    return;
                //}
                else
                {
                    List<Employees> list;
                    List<Departments> ListDepartment;
                    ListDepartment = RDepartment.GetListDepartment();
                    list = CRUD_Nhanvien.GetListWorker();
                    var qry = (from a in ListDepartment
                               where a.DepartmentName.Trim() == Department.ToString().Trim()
                               select a.DepartmentId).FirstOrDefault();
                    Employees nhanvien = new Employees();
                    nhanvien.EmployeeName = Name;
                    nhanvien.EmployeeId = Manv;
                    nhanvien.EmployeeDepartmentId = qry;
                    nhanvien.EmployeeSex = Gender.ToString();
                    nhanvien.EmployeeAdress = Adress;
                    nhanvien.EmployeeBirth = BirthDay;
                    var query = (from a in list
                                 where a.EmployeeId.Trim() == Manv.Trim()
                                 select a.ID).FirstOrDefault();
                    nhanvien.ID = query;
                    var boolResult = CRUD_Nhanvien.UpdateWorker(nhanvien);
                    if (boolResult == true)
                    {
                        StatusText = "Đã lưu thành công !!!";
                        Color = "blue";
                        // guwri update list
                        MessagingCenter.Send<UpdateUserViewModel>(this, "Update");
                    }
                    else
                    {
                        StatusText = "Thất bại !!!";
                        Color = "red";
                    }
                }
            }
            catch
            {

            }
            IsBusy = false;
        
        }
    }
}
