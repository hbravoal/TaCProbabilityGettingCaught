namespace Solver.BusinessLayer.Contracts.Required
{
    using Solver.Common.Models;
    using System.Collections.Generic;

    //Información: Interface para los Servicios que utilizan Programa como PARAMETRO.
    public interface IService<T> where T : class
    {
        List<T> GetAll();
        Response<T> Put(T entity);
        Response<T> Post(T entity);
        Response<T> Delete(T entity);
    }
}