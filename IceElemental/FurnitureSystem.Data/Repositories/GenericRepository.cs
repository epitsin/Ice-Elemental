namespace FurnitureSystem.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IFurnitureSystemDbContext context;

        private readonly IDbSet<T> set;

        public GenericRepository(IFurnitureSystemDbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public void Add(T entity)
        {
            var entry = this.AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        public IQueryable<T> All()
        {
            return this.set.AsQueryable();
        }

        public void Delete(T entity)
        {
            var entry = this.AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        public void Detach(T entity)
        {
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> conditions)
        {
            return this.All().Where(conditions);
        }

        public void Update(T entity)
        {
            var entry = this.AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            return entry;
        }
    }
}