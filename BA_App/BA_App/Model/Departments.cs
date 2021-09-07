using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace BA_App.Model
{
    public class Departments
    {
        private string _department = string.Empty;
        public int ID { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

    }
}
