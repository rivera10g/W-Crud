using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ewms.common.models.ViewModels
{
    public class vm_post
    {
    }

    public class vm_discrete_relocation
    {
        public string sReason { get; set; }
        public string sCod_User { get; set; }
        public string sCod_Product { get; set; }
        public string sLotNumber { get; set; }
        public decimal nQuantity { get; set; }
        public string sLocationSource { get; set; }
        public string sLocationTarget { get; set; }
        public string sUnit { get; set; }
        public string sCompany { get; set; }
    }
        

}