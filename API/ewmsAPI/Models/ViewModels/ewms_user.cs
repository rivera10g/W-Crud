using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ewmsAPI.Models.ViewModels
{
    public class ewms_user
    {
        public string user_id { get; set; }
        public int status_id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }        
    }

    public class loginInfo
    {
       public  string username { get; set; }
        public string password { get; set; }
        public  string dc { get; set; }
        public  string forklift { get; set; }
    }




}