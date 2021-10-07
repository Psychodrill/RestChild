using RestChild.Comon.ToExcel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace RestChild.Comon
{
    public interface IUnitOfWork : IDisposable
    {
        Database Database { get; }

        DbContext Context { get; }

        void AutoDetectChangesDisable();

        void AutoDetectChangesEnable();

        void Commit();

        void RollBack();

        void RollBack(bool allChanges);

        void BeginTransaction();

        void SaveChanges();

        Task SaveChangesAsync(CancellationToken token);

        IDbSet<T> GetSet<T>() where T : class, IEntityBase;

        void UpdateProperty<T, TProperty>(T entity, Expression<Func<T, TProperty>> expr) where T : class, IEntityBase;

        void Delete<T>(T entity) where T : class, IEntityBase;

        void Delete<T>(IEnumerable<T> entities) where T : class, IEntityBase;

        T AddEntity<T>(T entity, bool saveChanges = true) where T : class, IEntityBase;

        Task<T> AddEntityAsync<T>(T entity, CancellationToken token, bool saveChanges = true)
            where T : class, IEntityBase;

        T GetOrAttachEntity<T>(T entity) where T : class, IEntityBase;

        ICollection<T> GetOrAttachCollection<T>(ICollection<T> collection) where T : class, IEntityBase;

        T AttachEntity<T>(T entity) where T : class, IEntityBase;

        void DetachEntity<T>(T entity) where T : class, IEntityBase;

        void DetachAllEntitys();

        void EntityStateChange<T>(T entity, EntityState state) where T : class, IEntityBase;

        T Update<T>(T entity) where T : class, IEntityBase;

        long GetNextNumber(string key);

        void CopyDtoToEntity<T>(T sourceEntity, T destinationEntity) where T : class, IEntityBase;

        void CopyCollection<T>(ICollection<T> sourceCollection, ICollection<T> destinationCollection)
            where T : class, IEntityBase;

        ICollection<T> MergeCollection<T>(ICollection<T> sourceCollection, ICollection<T> destinationCollection,
            Action<T, T> mergeFunction = null) where T : class, IEntityBase;

        TransactionScope GetTransactionScope(IsolationLevel isolationLevel);

        TransactionScope GetTransactionScope();
    }
}
