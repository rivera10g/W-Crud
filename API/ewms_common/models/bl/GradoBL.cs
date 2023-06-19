using ewms.common.models.entity;
using ewms.common.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ewms.common.models.bl
{
    public class GradoBL
    {
        private Entities dbLocal = new Entities();

        public IEnumerable<GradoClass> getGrados()
        {
            try
            {
                var query = from p in dbLocal.TBL_GRADO
                            from j in dbLocal.TBL_PROFESOR.Where(x => x.ID == p.PROFESOR_ID).DefaultIfEmpty()
       
                            select new GradoClass()
                            {
                                id = p.ID,
                                nombre = p.NOMBRE,
                                profesor_id = p.PROFESOR_ID,
                                profesor_id_desc = j.NOMBRE + " " + j.APELLIDOS 
                            };
                return query.Distinct().ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Error. ", ex);
            }
        }


        public IEnumerable<GradoClass> getObjectList(int id)
        {
            throw new NotImplementedException();
        }

        public bcgResult addGrado(string nombre, int profesor_id)
        {
            bcgResult iResult = new bcgResult();
            entity.TBL_GRADO AddNew = new entity.TBL_GRADO();

            using (var dbContext = dbLocal.Database.BeginTransaction())
            {
                try
                {
                    //get last id
                    int ultimoID = dbLocal.TBL_GRADO.Max(a => a.ID);
                    int nuevoID = ultimoID + 1;

                    AddNew.ID = nuevoID;
                    AddNew.NOMBRE = nombre;
                    AddNew.PROFESOR_ID = profesor_id; //solo id's permitidos

                    dbLocal.TBL_GRADO.Add(AddNew);
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
                    dbLocal.TBL_GRADO.Remove(AddNew);
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

        public bcgResult editGrado(int id, string nombre, int profesor_id)
        {
            bcgResult iResult = new bcgResult();
            var Edit = new TBL_GRADO();

            try
            {
                Edit = dbLocal.TBL_GRADO.Where(x => x.ID == id).FirstOrDefault();

                if (Edit != null)
                {
                    Edit.NOMBRE = nombre;
                    Edit.PROFESOR_ID = profesor_id; //solo id's permitidos

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

        public bcgResult deleteGrado(int id)
        {
            bcgResult iResult = new bcgResult();

            try
            {
                var query = (from p in dbLocal.TBL_GRADO
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
                    var keyDuplicate = dbLocal.TBL_ALUMNO_GRADO.FirstOrDefault(ag => ag.GRADO_ID == id);
                    if (keyDuplicate != null)
                    {
                        iResult.message = "No se puede eliminar el registro debido a la relación existente en TBL_ALUMNO_GRADO";
                        iResult.success = false;
                        return iResult;
                    }

                    dbLocal.TBL_GRADO.Remove(query);
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