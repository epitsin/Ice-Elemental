namespace FurnitureSystem.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);

        IQueryable<T> All();

        void Delete(T entity);

        void Detach(T entity);

        IQueryable<T> SearchFor(Expression<Func<T, bool>> conditions);

        void Update(T entity);
    }
}