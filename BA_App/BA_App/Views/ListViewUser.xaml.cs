using BA_App.Enum;
using BA_App.Extentions;
using BA_App.Model;
using Syncfusion.DataSource;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace BA_App.Views
{
    public partial class ListViewUser : ContentPage
    {
        public ListViewUser()
        {
            InitializeComponent();
            var department = new ObservableCollection<string>();
            department.Add(EnumPicKerFilter.TatCa.GetEnumDescription());
            department.Add(EnumPicKerFilter.Mobile.GetEnumDescription());
            department.Add(EnumPicKerFilter.QA.GetEnumDescription());
            department.Add(EnumPicKerFilter.PhanMem.GetEnumDescription());
            FilterByDepartment.ItemsSource = department;
            SwipeListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "Name",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as Nhanvien);
                    //var result = item.Name[0];
                    return item.Ten[0].ToString();
                }
            });
        }

        private void Clicked_ManageNV(object sender, System.EventArgs e)
        {
            //String viewAccountInformation = ManagementOptions.ViewAccountInformation.GetEnumDescription();
            //String signUp = ManagementOptions.SignUp.GetEnumDescription();
            //string action = await DisplayActionSheet("Quản lý tài khoản", "Cancel", null, viewAccountInformation, signUp);
            //Debug.WriteLine("Action: " + action);
            //switch (action)
            //{
            //    // Chuyển đang trang quản lý tài khoản
            //    case "Xem thông tin tài khoản":
            //        MessagingCenter.Send<WorkDetailedList, string>(this, "ManageUser", "ManageUser");
            //        break;
            //    // Trở về trang màn hình đăng suất
            //    case "Đăng xuất":
            //        MessagingCenter.Send<WorkDetailedList, string>(this, "SignOut", "LoginPage");
            //        break;
           // }

        }

        private void picker_Opened(object sender, System.EventArgs e)
        {

        }

        private void picker_Closed(object sender, System.EventArgs e)
        {

        }

        private void picker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            btnFilterByDepartment.Text = e.NewValue.ToString();
            MessagingCenter.Send<ListViewUser, string>(this, "FilterByDepartment", e.NewValue.ToString());
            FilterByDepartment.IsOpen = false;

        }

        private void picker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            FilterByDepartment.IsOpen = false;
        }

        private void FilterWorker_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessagingCenter.Send<ListViewUser, string>(this, "FilterByName", e.NewTextValue);
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            FilterByDepartment.IsOpen = true;
        }
    }
}
