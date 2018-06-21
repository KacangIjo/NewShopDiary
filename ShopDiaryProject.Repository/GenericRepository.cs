using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository
{
    public abstract class GenericRepository<C, T> :IGenericRepository<T> where T : class,ISoftDelete where C : DbContext, new()
    //public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, ISoftDelete
    {
        //protected readonly ShopDiaryProject.EF.ShopDiaryDbContext _entities;

        private C _entities = new C();
        public C Context
        {

            get { return _entities; }
            set { _entities = value; }
        }


        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>().Where(i => i.IsDeleted == false);

            return query;
        }

        public virtual IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _entities.Set<T>().Where(i => i.IsDeleted == false).Where(predicate);
            return query;
        }

        public virtual T GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            T data = _entities.Set<T>().Where(i => i.IsDeleted == false).FirstOrDefault(predicate);
            return data;
        }

        public virtual bool Add(T entity)
        {
            try
            {
                _entities.Set<T>().Add(entity);
                this.Save();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public virtual bool AddRange(IEnumerable<T> entity)
        {
            try
            {
                _entities.Set<T>().AddRange(entity);
                this.Save();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public virtual bool Delete(T entity)
        {
            try
            {
                entity.IsDeleted = true;
                entity.DeletedDate = DateTime.Now;
                Edit(entity);
                this.Save();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public virtual bool Edit(T entity)
        {
            try
            {
                entity.ModifiedDate = DateTime.Now;
                _entities.Entry(entity).State = EntityState.Modified;
                this.Save();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public virtual bool Save()
        {
            try
            {
                _entities.SaveChanges();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Count(Expression<Func<T, bool>> match)
        {
            return _entities.Set<T>().Count(match);
        }

        public async virtual Task<bool> AddAsync(T entity)
        {
            try
            {
                _entities.Set<T>().Add(entity);
                await this.SaveAsync();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public async virtual Task<bool> AddRangeAsync(IEnumerable<T> entity)
        {
            try
            {
                _entities.Set<T>().AddRange(entity);
                await this.SaveAsync();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public async virtual Task<bool> DeleteAsync(T entity)
        {
            try
            {
                entity.IsDeleted = true;
                entity.DeletedDate = DateTime.Now;
                Edit(entity);
                await this.SaveAsync();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public async virtual Task<bool> EditAsync(T entity)
        {
            try
            {
                entity.ModifiedDate = DateTime.Now;
                _entities.Entry(entity).State = EntityState.Modified;
                await this.SaveAsync();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
                return false;
            }
            //catch (Exception e)
            //{
            //    return false;
            //}
        }

        public async virtual Task<bool> SaveAsync()
        {
            try
            {
                await _entities.SaveChangesAsync();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }
    }
    //public abstract class GenericRepository<C, T> :
    //IGenericRepository<T> where T : class,ISoftDelete where C : DbContext, new()
    //{

    //    private C _entities = new C();
    //    public C Context
    //    {

    //        get { return _entities; }
    //        set { _entities = value; }
    //    }

    //    public virtual IQueryable<T> GetAll()
    //    {

    //        IQueryable<T> query = _entities.Set<T>();
    //        return query;
    //    }

    //    public virtual T GetSingle(Guid id)
    //    {

    //        T query = _entities.Set<T>().FirstOrDefault(e => e.Id == id);
    //        return query;
    //    }

    //    public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
    //    {

    //        IQueryable<T> query = _entities.Set<T>().Where(predicate);
    //        return query;
    //    }

    //    public virtual bool Add(T entity)
    //    {
    //        try
    //        {
    //            _entities.Set<T>().Add(entity);
    //            Save();
    //            return true;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }

    //    public virtual void Delete(T entity)
    //    {
    //        entity.DeletedDate = DateTime.Now;
    //        entity.IsDeleted = true;
    //        Edit(entity);
    //    }

    //    public virtual void Edit(T entity)
    //    {
    //        _entities.Entry(entity).State = EntityState.Modified;
    //        Save();
    //    }

    //    private void Save()
    //    {
    //        _entities.SaveChanges();
    //    }
    //}
}
