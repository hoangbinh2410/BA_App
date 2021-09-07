using BA_App.DataAPI;
using BA_App.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace BA_App.Views
{
    public partial class UpdateUser : ContentPage
    {
        public UpdateUser()
        {
            InitializeComponent();
            var departments = RDepartment.GetListDepartment();
            if (departments.Count > 0)
            {
                var employeeCollection = new ObservableCollection<Departments>();
                foreach (var obj in departments)
                {
                    employeeCollection.Add(obj);
                }
                comboBoxDepartment.DataSource = employeeCollection;
            }
        }        
    }
}
