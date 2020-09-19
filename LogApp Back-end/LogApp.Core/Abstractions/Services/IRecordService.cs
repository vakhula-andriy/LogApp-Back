using System;
using System.Linq;
using LogApp.Core.Models;

namespace LogApp.Core.Abstractions.Services
{
    public interface IRecordService : IService<Record>
    {
        public IQueryable<Record> FilterByID(long startID = 0, long endID = long.MaxValue);
        public IQueryable<Record> FilterByFirstName(string name);
        public IQueryable<Record> FilterByLastName(string name);
        public IQueryable<Record> FilterByEmail(string email);
        public IQueryable<Record> FilterByAge(int startAge = 0, int endAge = 100);
        public IQueryable<Record> FilterByIP(string startIP, string endIP);
        public IQueryable<Record> FilterByTime(DateTimeOffset startTime, DateTimeOffset endTime);
        public int GetRecordsAmount();
    }
}
