using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Zop.Core;
using Zop.Core.Data;

namespace Zop.Data
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        //private readonly DbContext _context;
        private DbSet<T> _entities;

        #endregion
        #region Ctor
        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Utilities

        /// <summary>
        /// 获取完整的错误文本
        /// </summary>
        /// <param name="exc">Exception</param>
        /// <returns>Error</returns>
        protected string GetFullErrorText(InvalidOperationException exc)
        {
            //var msg = string.Empty;
            //foreach (var validationErrors in exc)
            //    foreach (var error in validationErrors.ValidationErrors)
            //        msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            //return msg;
            return exc.Message;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 通过Id获取entity
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public async virtual Task<T> GetById(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                throw new ArgumentNullException("Id");
            //see some suggested performance optimization (not tested)
            //http://stackoverflow.com/questions/11686225/dbset-find-method-ridiculously-slow-compared-to-singleordefault-on-id/11688189#comment34876113_11688189
            return await this.Entities.FindAsync(Id);
        }
        /// <summary>
        /// 插入entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public async virtual Task Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                await this.Entities.AddAsync(entity);

                await this._context.SaveChangesAsync();
            }
            catch (InvalidOperationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public async virtual Task Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                await this._context.SaveChangesAsync();
            }
            catch (InvalidOperationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public async virtual Task Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);

                await this._context.SaveChangesAsync();
            }
            catch (InvalidOperationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = this._context.Set<T>();
                return _entities;
            }
        }

        #endregion
    }
}
