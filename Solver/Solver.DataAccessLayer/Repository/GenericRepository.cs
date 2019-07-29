using System.Diagnostics.Contracts;

namespace Solver.DataAccessLayer.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;
    
    
    using Solver.DataAccessLayer.Contracts.Interfaces;
    
    using Solver.Common.Models;
    using Solver.Common.Helpers;
    using Solver.Entities.Contracts;

    public class GenericRepository<T> : IGenericRepository<T> where T : class,IEntity
    {
        private readonly ApplicationDataContext context;

        public GenericRepository(ApplicationDataContext context)
        {
            this.context = context;
        }


        #region Regular Members

        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>().AsNoTracking();
        }

        public IList<T> GetAllMatched(Expression<Func<T, bool>> match)
        {
            return this.context.Set<T>().Where(match).ToList();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }
            return queryable;
        }
        public IQueryable<T> GetByIdAndIncluding(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            //IQueryable<T> queryable = (context.Set<T>().AsNoTracking().Where(c => c.Id == id));
            //foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            //{
            //    queryable = queryable.Include<T, object>(includeProperty);
            //}
            //return queryable;
            return null;
        }
        public T GetById(int id)
        {
            return this.context.Set<T>()
                .AsNoTracking().FirstOrDefault(e => e.Id == id);

        }
        public T Find(Expression<Func<T, bool>> match)
        {
            return this.context.Set<T>()
                    .AsNoTracking().SingleOrDefault(match);

        }

        public IList<T> GetAllPaged(int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = this.context.Set<T>().Count();
            return this.context.Set<T>().Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }

        public int Count()
        {
            return this.context.Set<T>().Count();
        }
        public Response<T> Create(T entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                this.context.Set<T>().Add(entity);
                this.context.SaveChanges();
                return ResponseHelper<T>.SuccessResponse(
                    string.Format("Create: {0} with ID:{1} ", entity.GetType().FullName, entity.Id), entity);

            }
            catch (Exception ex)
            {
                return ResponseHelper<T>.ExceptionDatabase(ex, "Insertar", entity.GetType().FullName, entity);

            }
        }


        public Response<T> Delete(T entity)
        {
            this.context.Set<T>().Attach(entity);
            this.context.Set<T>().Remove(entity);
            try
            {
                this.context.SaveChanges();
                return ResponseHelper<T>.SuccessResponse(
                  string.Format("Registro Eliminado: {0}", entity.GetType().FullName), entity);
            }
            catch (Exception ex)
            {
                return ResponseHelper<T>.ExceptionDatabase(ex, "ELIMINAR", entity.GetType().FullName, entity);

            }

        }

        public Response<T> Update(T entity)
        {
            var entry = context.Set<T>().Update(entity);
            try
            {
                entry.State = EntityState.Modified;
                this.context.SaveChanges();
                return ResponseHelper<T>.SuccessResponse(
                string.Format("Registro Actualizado: {0}", entity.GetType().FullName), entity);
            }
            catch (Exception ex)
            {
                return ResponseHelper<T>.ExceptionDatabase(ex, "Actualizar", entity.GetType().FullName, entity);
            }
        }

        #endregion


        public async Task<T> GetByIdAsync(Guid id)
        {
            return await this.context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync();
            //return null;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public async Task<IList<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await this.context.Set<T>().Where(match).ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await this.context.Set<T>().FindAsync(id);

        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await this.context.Set<T>().SingleOrDefaultAsync(match);
        }

        public async Task<int> CountAsync()
        {
            return await this.context.Set<T>().CountAsync();
        }

        public async Task<bool> CreateAsync(T entity, bool saveChanges = false)
        {
            var rtn = await this.context.Set<T>().AddAsync(entity);
            if (saveChanges)
            {
                return await SaveAllAsync();

            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id, bool saveChanges = false)
        {
            this.context.Set<T>().Remove(GetById(id));
            if (saveChanges)
            {
                try
                {
                    await this.context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            return false;
        }


        public async Task<bool> DeleteAsync(T entity, bool saveChanges = false)
        {
            this.context.Set<T>().Remove(entity);
            if (saveChanges)
            {
                try
                {
                    await this.context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            return false;
        }


        public async Task<bool> UpdateAsync(T entity, bool saveChanges = false)
        {
            this.context.Set<T>().Update(entity);

            if (saveChanges)
            {
                await SaveAllAsync();
            }
            return false;
        }


        public async Task<bool> CreateAsync(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);

            return await SaveAllAsync();


        }

        public async Task<bool> UpdateAsync(T entity)
        {
            this.context.Set<T>().Update(entity);

            return await SaveAllAsync();


        }

        public async Task DeleteAsync(T entity)
        {
            this.context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            //return await this.context.Set<T>().AnyAsync(e => e.Id == id);
            return await this.context.Set<T>().AnyAsync(e => e.ToString() == (Convert.ToString(id)));

        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                await this.context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IList<T>> GetByAllIdAsync(Guid id)
        {
            return (await this.context.Set<T>()
                .AsNoTracking().ToListAsync());

            //return (await this.context.Set<T>()
            //    .AsNoTracking().Where(c => c.Id == id).ToListAsync());
        }
    }
}