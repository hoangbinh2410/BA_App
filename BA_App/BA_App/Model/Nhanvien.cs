using System;
using System.Collections.Generic;
using System.Text;

namespace BA_App.Model
{
     public class Nhanvien
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public string Manv { get; set; }
        public string Diachi { get; set; }
        public string Gioitinh { get; set; }
        public DateTime Ngaysinh { get; set; }
        public string Phong { get; set; }
        public string DateFix
        {
            get
            {
                return Ngaysinh.Day + "/" + Ngaysinh.Month + "/" + Ngaysinh.Year;
            }
            set { }
        }
        public int CreateByUser { get; set; }
        public int UpdatedByUser { get; set; }
    }
}
