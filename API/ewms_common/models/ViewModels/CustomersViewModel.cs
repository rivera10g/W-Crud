
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ewms.common.models.ViewModels
{
    public class CustomersViewModel
    {
        public string customer_id { get; set; }
        public string customer_text { get; set; }
        public int group_code { get; set; }
        public string company_id { get; set; }
        public string addres { get; set; }
        public string description { get; set; }
    }
    public class CustomersSelect3plViewModel
    {
        public string customer_id { get; set; }

    }
}
