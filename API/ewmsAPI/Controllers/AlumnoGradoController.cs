using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ewms.common.models.bl;
using ewms.common.models.ViewModels;
using System.Web.Http.Description;

namespace ewmsAPI.Controllers
{
    public class AlumnoGradoController : ApiController
    {
        AlumnoGradoBL blAG = new AlumnoGradoBL();

        //get
        [ActionName("getAlumnoGrado")]
        [HttpGet]
        [Route("api/AlumnoGrado/getAlumnoGrado")]
        public IEnumerable<AlumnoGradoClass> getAlumnoGrado()
        {
            return blAG.getAlumnoGrado();
        }

        //add
        [ActionName("addAlumnoG")]
        [HttpGet]
        [Route("api/AlumnoGrado/addAlumnoG")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult addAlumnoG(int alumno_id, int grado_id, string seccion)
        {
            return blAG.addAlumnoG(alumno_id, grado_id, seccion);
        }

        //update
        [ActionName("editAlumnoG")]
        [HttpGet]
        [Route("api/AlumnoGrado/editAlumnoG")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult editAlumnoG(int id, int alumno_id, int grado_id, string seccion)
        {
            return blAG.editAlumnoG(id, alumno_id, grado_id, seccion);
        }

        //delete
        [ActionName("deleteAlumnoG")]
        [HttpGet]
        [Route("api/AlumnoGrado/deleteAlumnoG")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult deleteAlumnoG(int id)
        {
            return blAG.deleteAlumnoG(id);
        }
    }
}