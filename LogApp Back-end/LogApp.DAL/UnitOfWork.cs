using LogApp.Core.Abstractions;
using LogApp.Core.Abstractions.Repositories;
using LogApp.DAL.Repositories;

namespace LogApp.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LogAppContext _context;
        private  IRecordRepository _records;

        public UnitOfWork(LogAppContext context)
        {
            _context = context;
        }
        public IRecordRepository Records { get => _records ??= new RecordRepository(_context); }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
