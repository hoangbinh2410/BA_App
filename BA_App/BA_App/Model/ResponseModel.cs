using System;
using System.Collections.Generic;
using System.Text;

namespace BA_App.Model
{
    public class ResponseModel<T>
    {

        public T data { get; set; }
        public bool success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
