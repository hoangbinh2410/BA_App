using System;
using System.Collections.Generic;
using System.Text;

namespace BA_App.Model
{
     public class Employees
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeAdress { get; set; }
        public string EmployeeSex { get; set; }
        public DateTime EmployeeBirth { get; set; }
        public int EmployeeDepartmentId { get; set; }
        public string DateFix
        {
            get
            {
                return EmployeeBirth.Day + "/" + EmployeeBirth.Month + "/" + EmployeeBirth.Year;
            }
            set { }
        }
        public int CreateByUser { get; set; }
        public int UpdatedByUser { get; set; }
    }
}
