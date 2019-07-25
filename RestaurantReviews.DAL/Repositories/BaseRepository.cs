using RestaurantReviews.DAL.Context;
using RestaurantReviews.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RestaurantReviews.DAL.Repositories {
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class {
        internal AppDbContext context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(AppDbContext context) {
            this.context = context;
            dbSet = this.context.Set<TEntity>();
        }

        public List<TEntity> GetAll() {
            return dbSet.ToList();
        }

        public List<TEntity> Get(Expression<Func<TEntity, bool>> predicate) {
            return dbSet.Where(predicate).ToList();
        }

        public TEntity GetById(object id) {
            return dbSet.Find(id);
        }

        public void Insert(TEntity entity) {
            dbSet.Add(entity);
        }

        public void Update(TEntity entity) {
            if (context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);
            var entry = context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(object id) {
            var deleted = dbSet.Find(id);
            dbSet.Remove(deleted);
        }
    }
}
