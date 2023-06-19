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
    public class ProfesorController : ApiController
    {
        ProfesorBL blProf = new ProfesorBL();

        //get
        [ActionName("getProfesores")]
        [HttpGet]
        [Route("api/Profesor/getProfesores")]
        public IEnumerable<ProfesorClass> getProfesores()
        {
            return blProf.getProfesores();
        }

        //add
        [ActionName("addProfesor")]
        [HttpGet]
        [Route("api/Profesor/addProfesor")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult addProfesor(string nombre, string apellidos, string genero)
        {
            return blProf.addProfesor(nombre, apellidos, genero);
        }

        //update
        [ActionName("editProfesor")]
        [HttpGet]
        [Route("api/Profesor/editProfesor")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult editProfesor(int id, string nombre, string apellidos, string genero)
        {
            return blProf.editProfesor(id, nombre, apellidos, genero);
        }

        //delete
        [ActionName("deleteProfesor")]
        [HttpGet]
        [Route("api/Profesor/deleteProfesor")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult deleteProfesor(int id)
        {
            return blProf.deleteProfesor(id);
        }
    }
}