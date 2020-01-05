using System;
using System.Linq;
using LogApp.Core.Abstractions.Repositories;
using LogApp.Core.Models;

namespace LogApp.DAL.Repositories
{
    public class RecordRepository : BaseRepository<Record>, IRecordRepository
    {
        public IQueryable<Record> _records { get; private set; }
        public RecordRepository(LogAppContext context) : base(context)
        {
            _records = _context.Set<Record>();
        }

        public override Record Add(Record record)
        {
            record.Timestamp = DateTimeOffset.Now;
            return base.Add(record);
        }
        public override IQueryable<Record> GetRange(int page, int pageSize = 25)
        {
            return _records.Skip(page * pageSize).Take(pageSize);
        }
        public void RangeByAge(int startAge, int endAge)
        {
            _records = _context.Set<Record>()
                .Where(record => record.Age >= startAge && record.Age <= endAge)
                .OrderBy(record => record.Age);
        }

        public void RangeByEmail(string startEmail, string endEmail)
        {
            _records = _context.Set<Record>()
                .Where(record => String.Compare(record.Email, startEmail) >= 0
                              && String.Compare(record.Email, endEmail) <= 0)
                .OrderBy(record => record.Email);
        }

        public void RangeByFirstName(string startName, string endName)
        {
            _records = _context.Set<Record>()
                .Where(record => String.Compare(record.FirstName, startName) >= 0
                              && String.Compare(record.FirstName, endName) <= 0)
                .OrderBy(record => record.FirstName);
        }

        public void RangeByID(long startID, long endID)
        {
            _records = _context.Set<Record>()
                .Where(record => record.ID >= startID && record.ID <= endID)
                .OrderBy(record => record.ID);
        }

        public void RangeByIP(string startIP, string endIP)
        {
            _records = _context.Set<Record>()
                .Where(record => String.Compare(record.IPAdress, startIP) >= 0
                              && String.Compare(record.IPAdress, endIP) <= 0)
                .OrderBy(record => record.IPAdress);
        }

        public void RangeByLastName(string startName, string endName)
        {
            _records = _context.Set<Record>()
                .Where(record => String.Compare(record.LastName, startName) >= 0
                              && String.Compare(record.LastName, endName) <= 0)
                .OrderBy(record => record.LastName);
        }

        public void RangeByTime(DateTimeOffset? startTime, DateTimeOffset? endTime)
        {
            _records = _context.Set<Record>()
                .Where(record => record.Timestamp >= startTime && record.Timestamp <= endTime)
                .OrderBy(record => record.Timestamp);
        }
    }
}
