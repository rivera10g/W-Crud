using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ewms.common.models.ViewModels
{
    public class AlumnoGradoClass
    {
        public int id { get; set; }
        public int? alumno_id { get; set; }
        public int? grado_id { get; set; }
        public string seccion { get; set; }
        
        public string alumno_id_desc { get; set; } //description id
        public string grado_id_desc { get; set; } //description id
    }
}
