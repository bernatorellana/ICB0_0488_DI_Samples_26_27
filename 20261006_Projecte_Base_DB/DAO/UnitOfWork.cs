using IDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class UnitOfWork : IDisposable
    {
        private readonly MyDBContext _context;

        public IDAOClient DAOClients { get; }

        public UnitOfWork(MyDBContext context)
        {
            _context = context;
            DAOClients = new DAOClient(_context);
        }

        public void BeginTransaction() => _context.Database.BeginTransaction();
        public void Commit() => _context.Database.CurrentTransaction?.Commit();

        public void Rollback() => _context.Database.CurrentTransaction?.Rollback();

        public void Dispose()
        {
         }
    }



}
