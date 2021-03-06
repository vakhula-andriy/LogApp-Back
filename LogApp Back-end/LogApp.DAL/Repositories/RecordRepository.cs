﻿using System;
using System.Linq;
using LogApp.Core.Abstractions.Repositories;
using LogApp.Core.Models;

namespace LogApp.DAL.Repositories
{
    public class RecordRepository : BaseRepository<Record>, IRecordRepository
    {
        public RecordRepository(LogAppContext context) : base(context)
        {
        }

        public override Record Add(Record record)
        {
            record.Timestamp = DateTimeOffset.Now;
            return base.Add(record);
        }

        public IQueryable<Record> GetRange(IQueryable<Record> records, int page, int pageSize = 25)
        {
            return records.Skip(page * pageSize).Take(pageSize);
        }

        public IQueryable<Record> RangeByAge(int startAge, int endAge)
        {
           return _context.Set<Record>()
                    .Where(record => record.Age >= startAge && record.Age <= endAge)
                    .OrderBy(record => record.Age);
        }

        public IQueryable<Record> RangeByEmail(string email)
        {
            return _context.Set<Record>()
                .Where(record => record.Email.Contains(email))
                .OrderBy(record => record.Email);
        }

        public IQueryable<Record> RangeByFirstName(string name)
        {
            return _context.Set<Record>()
                .Where(record => record.FirstName.Contains(name))
                .OrderBy(record => record.FirstName);
        }

        public IQueryable<Record> RangeByID(long startID, long endID)
        {
            return _context.Set<Record>()
                    .Where(record => record.ID >= startID && record.ID <= endID)
                    .OrderBy(record => record.ID);
        }

        public IQueryable<Record> RangeByIP(string startIP, string endIP)
        {
            return _context.Set<Record>()
                .Where(record => String.Compare(record.IPAdress, startIP) >= 0
                              && String.Compare(record.IPAdress, endIP) <= 0)
                .OrderBy(record => record.IPAdress);
        }

        public IQueryable<Record> RangeByLastName(string name)
        {
            return _context.Set<Record>()
                .Where(record => record.LastName.Contains(name))
                .OrderBy(record => record.LastName);
        }

        public IQueryable<Record> RangeByTime(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            return _context.Set<Record>()
                .Where(record => record.Timestamp >= startTime && record.Timestamp <= endTime)
                .OrderBy(record => record.Timestamp);
        }
    }
}
