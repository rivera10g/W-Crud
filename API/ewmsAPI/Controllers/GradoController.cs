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
    public class GradoController : ApiController
    {
        GradoBL blGrad = new GradoBL();

        //get
        [ActionName("getGrados")]
        [HttpGet]
        [Route("api/Grado/getGrados")]
        public IEnumerable<GradoClass> getGrados()
        {
            return blGrad.getGrados();
        }


        //add
        [ActionName("addGrado")]
        [HttpGet]
        [Route("api/Grado/addGrado")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult addGrado(string nombre, int profesor_id)
        {
            return blGrad.addGrado(nombre, profesor_id);
        }

        //update
        [ActionName("editGrado")]
        [HttpGet]
        [Route("api/Grado/editGrado")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult editGrado(int id, string nombre, int profesor_id)
        {
            return blGrad.editGrado(id, nombre, profesor_id);
        }

        //delete
        [ActionName("deleteGrado")]
        [HttpGet]
        [Route("api/Grado/deleteGrado")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult deleteGrado(int id)
        {
            return blGrad.deleteGrado(id);
        }
    }
}