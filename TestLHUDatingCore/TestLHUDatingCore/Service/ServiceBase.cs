using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestLHUDatingCore.Service
{
    public interface IServiceBase<TEntity, TKey> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity GetById(TKey key);
        TEntity Insert(TEntity entity);
        void Update(TKey key, TEntity entity);
        void DeleteById(TKey key, string userSession = null);
    }
}
