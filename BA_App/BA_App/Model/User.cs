using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BA_App.Model
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string UserId { get; set; }
    }
}
