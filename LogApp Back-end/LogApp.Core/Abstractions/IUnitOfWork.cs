using LogApp.Core.Abstractions.Repositories;

namespace LogApp.Core.Abstractions
{
    public interface IUnitOfWork
    {
        public IRecordRepository Records { get; }
        public void Save();
    }
}
