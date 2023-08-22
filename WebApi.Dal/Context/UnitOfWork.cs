using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dal.Entities;
using WebApi.Dal.Repositories;

namespace WebApi.Dal.Context
{
    public class UnitOfWork : IDisposable
    {
        private readonly ProjectContext _appContext;
        private GenericRepository<Contact> _contactRepository;

        public UnitOfWork(ProjectContext appContext)
        {
            _appContext = appContext;
        }
        public GenericRepository<Contact> ContactRepository
        {
            get
            {
                return _contactRepository ?? new GenericRepository<Contact>(_appContext);
            }
        }

        public void Save()
        {
            _appContext.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _appContext.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
