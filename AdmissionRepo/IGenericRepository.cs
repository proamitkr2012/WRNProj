using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionRepo 
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
      /*  TEntity Get(int Id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);*/

        Task<TEntity> GetByIdAsync(string  id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(string id);

       

    }
}
