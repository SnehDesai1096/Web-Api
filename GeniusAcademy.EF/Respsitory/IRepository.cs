using GeniusAcademy.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeniusAcademy.EF.Respsitory
{
    public interface IRepository<T> where T : class, IEntity
    {   
        Task<List<T>> GetAll();
        Task<T> Get(long id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(long id);
    }
}
