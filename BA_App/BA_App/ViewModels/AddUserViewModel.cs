using BA_App.DataAPI;
using BA_App.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BA_App.ViewModels
{
    public class AddUserViewModel : ViewModelBase
    {
        private string _statusText  = string.Empty;
        private string _color = string.Empty;
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
        public string Gender { get; set; }
        public string Department { get; set; }
        public string Manv { get; set; }
        public string Adress { get; set; }

        public DateTime BirthDay = DateTime.Now;
        public Command AddWorkerCommand { get; }
        public AddUserViewModel()
        {
            AddWorkerCommand = new Command(AddWorkerClicked);
            Title = "Thêm công nhân";

        }
        private async void AddWorkerClicked(object obj)
        {
            StatusText = "";
            IsBusy = true;
            await Task.Delay(1000);
            //kiểm tra thông tin đã điền đầy đúng chưa?
            if (string.IsNullOrEmpty(Name) || Department == null || Gender == "")
            {
                StatusText = "Điền đầy đủ thông tin !!!";
                Color = "red";
                IsBusy = false;
                return;
            }
            if (Gender != "Nam" && (Gender != "Nữ"))
            {
                StatusText = "Thông tin chưa chính xác !!!";
                Color = "red";
                IsBusy = false;
                return;
            }
            Nhanvien nhanvien = new Nhanvien();
            nhanvien.Ten = Name;
            nhanvien.Gioitinh = Gender;
            nhanvien.Ngaysinh = BirthDay;
            nhanvien.Phong = Department;
            nhanvien.Manv = Manv;
            nhanvien.Diachi = Adress;
            try
            {              
                var boolResult = CRUD_Nhanvien.AddNewWorker(nhanvien);
                if (boolResult== true)
                {
                    StatusText = "Đã thêm thành công !!!";
                    Color = "blue";
                    MessagingCenter.Send<AddUserViewModel>(this, "Add");
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
