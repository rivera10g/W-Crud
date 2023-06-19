using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ewms.common.models.entity;
using ewms.common.models.ViewModels;

namespace ewms.common.models.bl
{
    public class ProfesorBL
    {
        private Entities dbLocal = new Entities();

        public IEnumerable<ProfesorClass> getProfesores()
        {
            try
            {
                var query = from p in dbLocal.TBL_PROFESOR
                            select new ProfesorClass()
                            {
                                id = p.ID,
                                nombre = p.NOMBRE,
                                apellidos = p.APELLIDOS,
                                genero = p.GENERO,
                                nombre_desc = p.NOMBRE + " " + p.APELLIDOS
                            };
                return query.Distinct().ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Error. ", ex);
            }
        }

        public bcgResult addProfesor(string nombre, string apellidos, string genero)
        {
            bcgResult iResult = new bcgResult();
            entity.TBL_PROFESOR AddNew = new entity.TBL_PROFESOR();

            using (var dbContext = dbLocal.Database.BeginTransaction())
            {
                try
                {
                    //get last id
                    int ultimoID = dbLocal.TBL_PROFESOR.Max(a => a.ID);
                    int nuevoID = ultimoID + 1;

                    AddNew.ID = nuevoID;
                    AddNew.NOMBRE = nombre;
                    AddNew.APELLIDOS = apellidos;
                    AddNew.GENERO = genero;


                    dbLocal.TBL_PROFESOR.Add(AddNew);
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
                    dbLocal.TBL_PROFESOR.Remove(AddNew);
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

        public bcgResult editProfesor(int id, string nombre, string apellidos, string genero)
        {
            bcgResult iResult = new bcgResult();
            var Edit = new TBL_PROFESOR();

            try
            {
                Edit = dbLocal.TBL_PROFESOR.Where(x => x.ID == id).FirstOrDefault();

                if (Edit != null)
                {
                    Edit.NOMBRE = nombre;
                    Edit.APELLIDOS = apellidos;
                    Edit.GENERO = genero;

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

        public bcgResult deleteProfesor(int id)
        {
            bcgResult iResult = new bcgResult();

            try
            {
                var query = (from p in dbLocal.TBL_PROFESOR
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
                    var keyDuplicate = dbLocal.TBL_GRADO.FirstOrDefault(ag => ag.PROFESOR_ID == id);
                    if (keyDuplicate != null)
                    {
                        iResult.message = "No se puede eliminar el registro debido a la relación existente en TBL_GRADO";
                        iResult.success = false;
                        return iResult;
                    }

                    dbLocal.TBL_PROFESOR.Remove(query);
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