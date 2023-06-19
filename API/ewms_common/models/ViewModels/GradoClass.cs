using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ewms.common.models.ViewModels
{
    public class GradoClass
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int? profesor_id { get; set; }
        public string profesor_id_desc { get; set; } //description id
    }
}
