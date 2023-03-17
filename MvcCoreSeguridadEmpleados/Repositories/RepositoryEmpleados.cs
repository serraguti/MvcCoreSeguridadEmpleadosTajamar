using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            return await
                this.context.Empleados.ToListAsync();
        }

        public async Task<Empleado> FindEmpleadoAsync(int idempleado)
        {
            return await
                this.context.Empleados.FirstOrDefaultAsync
                (x => x.IdEmpleado == idempleado);
        }

        public async Task<List<Empleado>> 
            GetEmpleadosDepartamentoAsync(int iddepartamento)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.Departamento == iddepartamento
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task UpdateSalariosDepartamento
            (int iddepartamento, int incremento)
        {
            List<Empleado> empleados =
                await this.GetEmpleadosDepartamentoAsync(iddepartamento);
            foreach (Empleado empleado in empleados)
            {
                empleado.Salario += incremento;
            }
            await this.context.SaveChangesAsync();
        }
    }
}
