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
    public class AlumnoController : ApiController
    {
        AlumnoBL blAlumn = new AlumnoBL();

        //get 
        [ActionName("getAlumnos")]
        [HttpGet]
        [Route("api/Alumno/getAlumnos")]
        public IEnumerable<AlumnoClass> getAlumnos()
        {
            return blAlumn.getAlumnos();
        }

        //add
        [ActionName("addAlumno")]
        [HttpGet]
        [Route("api/Alumno/addAlumno")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult addAlumno(string nombre, string apellidos, string genero, string f_nacimiento)
        {
            return blAlumn.addAlumno(nombre, apellidos, genero, f_nacimiento);
        }

        //update
        [ActionName("editAlumno")]
        [HttpGet]
        [Route("api/Alumno/editAlumno")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult editAlumno(int id, string nombre, string apellidos, string genero, string f_nacimiento)
        {
            return blAlumn.editAlumno(id, nombre, apellidos, genero, f_nacimiento);
        }

        //delete
        [ActionName("deleteAlumno")]
        [HttpGet]
        [Route("api/Alumno/deleteAlumno")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult deleteAlumno(int id)
        {
            return blAlumn.deleteAlumno(id);
        }
    }
}