using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rishvi.Modules.Core.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //IEnumerable<TEntity> GetWithSQL(string query, params object[] parameters);

        void Insert(TEntity entity);
        void Delete(int Id);
        void Delete(TEntity entity);

        void Delete(Guid id);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(object id);
        void Update(TEntity entity);
        void Update1(TEntity entity);
        //IQueryable<TEntity> Table { get; }
        //IQueryable<TEntity> AsNoTracking { get; }
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly DbSet<TEntity> _dbSet;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Insert(TEntity entity)
        {
            _unitOfWork.Context.Set<TEntity>().Add(entity);
        }

        public void Delete(int id)
        {
            //TEntity existing = _unitOfWork.Context.Set<TEntity>().Find(entity);
            TEntity existing = _unitOfWork.Context.Set<TEntity>().Find(id);
            if (existing != null) _unitOfWork.Context.Set<TEntity>().Remove(existing);
        }

        public void Delete(Guid id)
        {
            //TEntity existing = _unitOfWork.Context.Set<TEntity>().Find(entity);
            TEntity existing = _unitOfWork.Context.Set<TEntity>().Find(id);
            if (existing != null) _unitOfWork.Context.Set<TEntity>().Remove(existing);
        }

        public void Delete(TEntity entity)
        {
            //TEntity existing = _unitOfWork.Context.Set<TEntity>().Find(entity);
            TEntity existing = _unitOfWork.Context.Set<TEntity>().Find(entity);
            if (existing != null) _unitOfWork.Context.Set<TEntity>().Remove(existing);
        }

        public IQueryable<TEntity> Get()
        {
            return _unitOfWork.Context.Set<TEntity>().AsQueryable<TEntity>();
        }

        public TEntity GetById(object id)
        {
            return _unitOfWork.Context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _unitOfWork.Context.Set<TEntity>().Where(predicate).AsQueryable<TEntity>();
        }

        public void Update(TEntity entity)
        {
            _unitOfWork.Context.Set<TEntity>().Attach(entity);
            //_unitOfWork.Context.Entry(entity).State = EntityState.Modified;
            _unitOfWork.Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Update1(TEntity entity)
        {
            _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
            _unitOfWork.Context.Set<TEntity>().Attach(entity);
        }

        //public IQueryable<TEntity> Table => _dbSet;
        //public IQueryable<TEntity> AsNoTracking => _dbSet.AsNoTracking();
    }
    //public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    //{
    //    private ApplicationDbContext context;
    //    private DbSet<TEntity> dbSet;

    //    public Repository(ApplicationDbContext context)
    //    {
    //        this.context = context;
    //        dbSet = this.context.Set<TEntity>();
    //    }

    //    public IEnumerable<TEntity> GetWithSQL(string query, params object[] parameter)
    //    {
    //        return dbSet.FromSqlRaw(query, parameter);
    //    }
    //    public IEnumerable<TEntity> Get()
    //    {
    //        return dbSet.ToList();
    //    }

    //    public TEntity GetById(object id)
    //    {
    //        return dbSet.Find(id);
    //    }

    //    public void Insert(TEntity entity)
    //    {
    //        dbSet.Add(entity);
    //    }

    //    public void Update(TEntity entity)
    //    {
    //        dbSet.Attach(entity);
    //        context.Entry(entity).State = EntityState.Modified;
    //    }

    //    public void Delete(object id)
    //    {
    //        TEntity entity = dbSet.Find(id);
    //        Delete(entity);
    //    }
    //    public void Delete(TEntity entity)
    //    {
    //        if (context.Entry(entity).State == EntityState.Detached)
    //        {
    //            dbSet.Attach(entity);
    //        }

    //        dbSet.Remove(entity);
    //    }

    //    public void Save()
    //    {
    //        context.SaveChanges();
    //    }
    //}
}
