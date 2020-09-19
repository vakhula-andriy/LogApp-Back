using System;
using System.Collections.Generic;
using System.Linq;
using LogApp.Core.Models;

namespace LogApp.Core.Abstractions.Repositories
{
    public interface IRecordRepository : IRepository<Record>
    {
        public IQueryable<Record> GetRange(IQueryable<Record> records, int page, int pageSize = 25);
        public IQueryable<Record> RangeByID(long startID, long endID);
        public IQueryable<Record> RangeByFirstName(string name);
        public IQueryable<Record> RangeByLastName(string name);
        public IQueryable<Record> RangeByEmail(string email);
        public IQueryable<Record> RangeByAge(int startAge, int endAge);
        public IQueryable<Record> RangeByIP(string startIP, string endIP);
        public IQueryable<Record> RangeByTime(DateTimeOffset startTime, DateTimeOffset endTime);
    }
}
