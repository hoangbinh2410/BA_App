using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace BA_App.Views
{
    public partial class AddUser : ContentPage
    {
        public AddUser()
        {
            InitializeComponent();
            //var departments = InfomationDepartment.GetInfoDepartment();
            //if (departments.Count > 0)
            //{
            //    var employeeCollection = new ObservableCollection<Departments>();
            //    foreach (var obj in departments)
            //    {
            //        employeeCollection.Add(obj);
            //    }
            //    comboBoxDepartment.DataSource = employeeCollection;
            //}
        }
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newValue = e.NewTextValue;
            if (int.TryParse(newValue, out int value))
            {
                return;
            }
            else
            {
                //textOld.Text = "";
            }
        }
    }
}
