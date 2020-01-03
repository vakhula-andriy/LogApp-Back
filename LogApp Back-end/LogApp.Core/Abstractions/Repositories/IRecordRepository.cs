using System;
using System.Linq;
using LogApp.Core.Models;

namespace LogApp.Core.Abstractions.Repositories
{
    public interface IRecordRepository : IRepository<Record>
    {
        public IQueryable<Record> _records { get; }
        public void RangeByID(long startID, long endID);
        public void RangeByFirstName(string startName, string endName);
        public void RangeByLastName(string startName, string endName);
        public void RangeByEmail(string startEmail, string endEmail);
        public void RangeByAge(int startAge, int endAge);
        public void RangeByIP(string startIP, string endIP);
        public void RangeByTime(DateTimeOffset startTime, DateTimeOffset endTime);
    }
}
