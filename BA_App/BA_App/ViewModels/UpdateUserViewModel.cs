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
    public class UpdateUserViewModel : ViewModelBase
    {
        private string _statusText = string.Empty;
        private string _color = string.Empty;
        private string _name = string.Empty;
        private string _manv = string.Empty;
        private string _adress = string.Empty;
        private string _department = string.Empty;
        private DateTime _birthDay = DateTime.Now;
        private string _gender;
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
        public string Gender
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
        public string Department
        {
            get { return _department; }
            set { SetProperty(ref _department, value); }
        }
        public string Adress
        {
            get { return _adress; }
            set { SetProperty(ref _adress, value); }
        }
        //Binding dữ liệu 2 chiều DateJoin
        public DateTime BirthDay
        {
            get { return _birthDay; }
            set { SetProperty(ref _birthDay, value); }
        }
        public Command UpdateCommand { get; }
        private Nhanvien _objWorker { get; set; }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _objWorker = parameters.GetValue<Nhanvien>("Nhanvien");
            //Binding dữ liệu khi chuyển trang
            Name = _objWorker.Ten;
            Gender = _objWorker.Gioitinh;
            Manv = _objWorker.Manv;
            Department = _objWorker.Phong;
            BirthDay = _objWorker.Ngaysinh;
            Adress = _objWorker.Diachi;
        }
        public UpdateUserViewModel()
        {
            UpdateCommand = new Command(UpdateClicked);
            Title = "Sửa thông tin";
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
                if(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Manv) || string.IsNullOrEmpty(Department) || string.IsNullOrEmpty(Adress) || (string)Gender == "")
                {
                    StatusText = "Điền đầy đủ thông tin !!!";
                    Color = "red";
                    IsBusy = false;
                    return;
                }
                if ((string)Gender != "Nam" && ((string)Gender != "Nữ"))
                {
                    StatusText = "Thông tin chưa chính xác !!!";
                    Color = "red";
                    IsBusy = false;
                    return;
                }
                List<Nhanvien> list ;
                list = CRUD_Nhanvien.GetListWorker();
                Nhanvien nhanvien = new Nhanvien();
                nhanvien.Ten = Name;
                nhanvien.Manv = Manv;
                nhanvien.Phong = Department;
                nhanvien.Gioitinh = Gender;
                nhanvien.Diachi = Adress;
                nhanvien.Ngaysinh = BirthDay;
                var query = (from a in list
                             where a.Manv.Trim() == Manv.Trim()
                             select a.ID).FirstOrDefault();
                nhanvien.ID = query;
                var boolResult = CRUD_Nhanvien.UpdateWorker(nhanvien);
                if (boolResult == true)
                {
                    StatusText = "Đã lưu thành công !!!";
                    Color = "blue";
                    // guwri update list
                    MessagingCenter.Send<UpdateUserViewModel, int>(this, "Update", _objWorker.ID);
                }
                else
                {
                    StatusText = "Thất bại !!!";
                    Color = "red";
                }
            }
            catch
            {

            }
            IsBusy = false;
        }
    }
}
