using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RestaurantReviews.Models.Contracts {
    public interface IBaseRepository<TEntity> where TEntity : class {
        List<TEntity> GetAll();
        List<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
    }
}
