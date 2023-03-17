using MvcCoreSeguridadEmpleados.Data;
using MvcCoreSeguridadEmpleados.Models;

namespace MvcCoreSeguridadEmpleados.Repositories
{
    public class RepositoryEmpleados
    {
        private EmpleadosContext context;

        public RepositoryEmpleados(EmpleadosContext context)
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleados()
        {
            return this.context.Empleados.ToList();
        }

        public Empleado FindEmpleado(int idempleado)
        {
            return
                this.context.Empleados.FirstOrDefault
                (x => x.IdEmpleado == idempleado);
        }

        public List<Empleado> GetEmpleadosDepartamento(int iddepartamento)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.Departamento == iddepartamento
                           select datos;
            return consulta.ToList();
        }
    }
}
