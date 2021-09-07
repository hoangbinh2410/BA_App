using System;
using System.Collections.Generic;
using System.Text;

namespace BA_App.Model
{
   public class DataEmployee
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeAdress { get; set; }
        public string EmployeeSex { get; set; }
        public DateTime EmployeeBirth { get; set; }
        public string EmployeeDepartmentName { get; set; }
        public string DateFix
        {
            get
            {
                return EmployeeBirth.Day + "/" + EmployeeBirth.Month + "/" + EmployeeBirth.Year;
            }
            set { }
        }
    }
}
