using ewms.common.models.entity;
using ewms.common.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ewms.common.models.bl
{
    public class AlumnoGradoBL
    {
        private Entities dbLocal = new Entities();

        public IEnumerable<AlumnoGradoClass> getAlumnoGrado()
        {
            try
            {
                var query = from p in dbLocal.TBL_ALUMNO_GRADO
                            from k in dbLocal.TBL_ALUMNO.Where(x => x.ID == p.ALUMNO_ID).DefaultIfEmpty()
                            from j in dbLocal.TBL_GRADO.Where(x => x.ID == p.GRADO_ID).DefaultIfEmpty()
                            
                            select new AlumnoGradoClass()
                            {
                                id = p.ID,
                                alumno_id = p.ALUMNO_ID,
                                grado_id = p.GRADO_ID,
                                seccion = p.SECCION,
                                alumno_id_desc = k.NOMBRE + " " + k.APELLIDOS,
                                grado_id_desc = j.NOMBRE
                            };
                return query.Distinct().ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Error. ", ex);
            }
        }

        public bcgResult addAlumnoG(int alumno_id, int grado_id, string seccion)
        {
            bcgResult iResult = new bcgResult();
            entity.TBL_ALUMNO_GRADO AddNew = new entity.TBL_ALUMNO_GRADO();

            using (var dbContext = dbLocal.Database.BeginTransaction())
            {
                try
                {
                    //get last id
                    int ultimoID = dbLocal.TBL_ALUMNO_GRADO.Max(a => a.ID);
                    int nuevoID = ultimoID + 1;

                    AddNew.ID = nuevoID;
                    AddNew.ALUMNO_ID = alumno_id; //solo id's permitidos
                    AddNew.GRADO_ID = grado_id; //solo id's permitidos
                    AddNew.SECCION = seccion;

                    dbLocal.TBL_ALUMNO_GRADO.Add(AddNew);
                    int iresultado = dbLocal.SaveChanges();

                    if (iresultado == 1)
                    {
                        dbContext.Commit();
                        iResult.success = true;
                    }
                    else
                    {
                        dbContext.Rollback();
                        iResult.success = false;
                    }

                }
                catch (Exception ex)
                {
                    dbLocal.TBL_ALUMNO_GRADO.Remove(AddNew);
                    try
                    {
                        dbContext.Rollback();
                    }
                    #pragma warning disable CS0168
                    catch (Exception exTrans)
                    #pragma warning restore CS0168
                    {

                    }

                    if (ex.InnerException.InnerException == null)
                    {
                        iResult.success = false;
                    }
                    else
                    {
                        if (ex.InnerException.InnerException.Message.Contains("Violation of PRIMARY KEY constraint"))
                        {
                            iResult.success = false; // duplicate
                        }
                        else
                        {
                            iResult.success = false; // error
                        }
                    }
                }
            }
            return iResult;
        }

        public bcgResult editAlumnoG(int id, int alumno_id, int grado_id, string seccion)
        {
            bcgResult iResult = new bcgResult();
            var Edit = new TBL_ALUMNO_GRADO();

            try
            {
                Edit = dbLocal.TBL_ALUMNO_GRADO.Where(x => x.ID == id).FirstOrDefault();

                if (Edit != null)
                {
                    Edit.ALUMNO_ID = alumno_id; //solo id's permitidos
                    Edit.GRADO_ID = grado_id; //solo id's permitidos

                    dbLocal.SaveChanges();
                    iResult.success = true;
                }
                else
                {
                    iResult.success = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Error", ex);
            }

            return iResult;
        }

        public bcgResult deleteAlumnoG(int id)
        {
            bcgResult iResult = new bcgResult();

            try
            {
                var query = (from p in dbLocal.TBL_ALUMNO_GRADO
                             where p.ID == id
                             select p).FirstOrDefault();

                if (query == null)
                {
                    iResult.message = "Registro no econtrado";
                    iResult.success = false;
                    return iResult;
                }
                else
                {
                    dbLocal.TBL_ALUMNO_GRADO.Remove(query);
                    dbLocal.SaveChanges();
                    iResult.success = true;
                }

            }
            #pragma warning disable CS0168
            catch (Exception ex)
            #pragma warning restore CS0168
            {
                iResult.success = false;
                iResult.message = "Error al eliminar registro";
            }
            return iResult;
        }
    }
}
