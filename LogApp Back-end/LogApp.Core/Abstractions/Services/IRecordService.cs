using System;
using LogApp.Core.Models;

namespace LogApp.Core.Abstractions.Services
{
    public interface IRecordService : IService<Record>
    {
        public void FilterByID(long startID = 0, long endID = long.MaxValue);
        public void FilterByFirstName(string startName, string endName);
        public void FilterByLastName(string startName, string endName);
        public void FilterByEmail(string startName, string endName);
        public void FilterByAge(int startAge = 0, int endAge = 100);
        public void FilterByIP(string startIP, string endIP);
        public void FilterByTime(DateTimeOffset startTime, DateTimeOffset endTime);
        public int GetRecordsAmount();
    }
}
