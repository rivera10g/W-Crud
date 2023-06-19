using ewms.common.models.entity;
using ewms.common.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ewms.common.models.bl
{
    public class AlumnoBL
    {
        private Entities dbLocal = new Entities();

        public IEnumerable<AlumnoClass> getAlumnos()
        {
            try
            {
                var query = from p in dbLocal.TBL_ALUMNO
                            select new AlumnoClass()
                            {
                                id = p.ID,
                                nombre = p.NOMBRE,
                                apellidos = p.APELLIDOS,
                                genero = p.GENERO,
                                f_nacimiento = p.F_NACIMIENTO,
                                nombre_desc = p.NOMBRE + " " + p.APELLIDOS
                            };
                return query.Distinct().ToList();
 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Error", ex);
            }
        }

        public bcgResult addAlumno(string nombre, string apellidos, string genero, string f_nacimiento)
        {
            bcgResult iResult = new bcgResult();
            entity.TBL_ALUMNO AddNew = new entity.TBL_ALUMNO();

            using (var dbContext = dbLocal.Database.BeginTransaction())
            {
                try
                {
                    //get last id
                    int ultimoID = dbLocal.TBL_ALUMNO.Max(a => a.ID);
                    int nuevoID = ultimoID + 1;

                    AddNew.ID = nuevoID;
                    AddNew.NOMBRE = nombre;
                    AddNew.APELLIDOS = apellidos;
                    AddNew.GENERO = genero;
                    AddNew.F_NACIMIENTO = f_nacimiento;

                    dbLocal.TBL_ALUMNO.Add(AddNew);
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
                    dbLocal.TBL_ALUMNO.Remove(AddNew);
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
       
        public bcgResult editAlumno(int id, string nombre, string apellidos, string genero, string f_nacimiento)
        {
            bcgResult iResult = new bcgResult();
            var Edit = new TBL_ALUMNO();

            try
            {
                Edit = dbLocal.TBL_ALUMNO.Where(x => x.ID == id).FirstOrDefault();

                if (Edit != null)
                {
                    Edit.NOMBRE = nombre;
                    Edit.APELLIDOS = apellidos;
                    Edit.GENERO = genero;
                    Edit.F_NACIMIENTO = f_nacimiento;

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

        public bcgResult deleteAlumno(int id)
        {
            bcgResult iResult = new bcgResult();

            try
            {
                var query = (from p in dbLocal.TBL_ALUMNO
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
                    var keyDuplicate = dbLocal.TBL_ALUMNO_GRADO.FirstOrDefault(ag => ag.ALUMNO_ID == id);
                    if (keyDuplicate != null)
                    {
                        iResult.message = "No se puede eliminar el registro debido a la relación existente en TBL_ALUMNO_GRADO";
                        iResult.success = false;
                        return iResult;
                    }

                    dbLocal.TBL_ALUMNO.Remove(query);
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