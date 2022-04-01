using ECommerceProject.Business.IUnitOfWorks;
using ECommerceProject.Core.Entities;
using ECommerceProject.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceProject.Business.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ECommerceDbContext _dbContext;
        private bool disposed;

        public UnitOfWork(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CommitAsync()
        {

            bool returnValue = true;
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));

                    //yeni kayıt(lar) atılırken; recordDate şuanki tarih atılıyor
                    var addedEntities = _dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();

                    addedEntities.ForEach(E =>
                    {
                        E.Property(nameof(BaseEntity.RecordDate)).CurrentValue = DateTime.Now;
                    });

                    //güncelleme yapılan kayıt(lar)'da; updateDate kolonuna suanki tarih verisi atılıyor
                    var editedEntities =
                        _dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();

                    editedEntities.ForEach(e =>
                    {
                        e.Property(nameof(BaseEntity.UpdateDate)).CurrentValue = DateTime.Now;
                    });

                    var result = await _dbContext.SaveChangesAsync();

                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    var hata = ex;
                    //Log Exception Handling message                      
                    returnValue = false;
                    await dbContextTransaction.RollbackAsync();
                }
            }

            return returnValue;

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            disposed = true;
        }

    }
}
