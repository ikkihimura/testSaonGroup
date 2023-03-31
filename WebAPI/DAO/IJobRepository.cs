using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DAO
{
    //Repository for access to the models in the inmemory database
    //we change!
    public interface IJobRepository<TEntity, U> where TEntity : class
    {

        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(U id);
        Task<long> Add(TEntity b);
        Task<long> Update(U id, TEntity b);
        Task<long> Delete(U id);
    }
}
