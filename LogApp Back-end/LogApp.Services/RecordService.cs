using System;
using System.Linq;
using System.Collections.Generic;
using FluentValidation;
using LogApp.Core.Abstractions.Services;
using LogApp.Core.Abstractions.Repositories;
using LogApp.Core.Models;
using LogApp.Core.Validators;
using LogApp.DAL;

namespace LogApp.Services
{
    public class RecordService : IRecordService
    {
        private IRecordRepository _recordRepository;
        private LogAppContext _context;
        private readonly RecordValidator _recordsValidator;
        public RecordService(IRecordRepository recordRepository, LogAppContext logAppContext)
        {
            _recordRepository = recordRepository;
            _context = logAppContext;
            _recordsValidator = new RecordValidator();
        }

        public void Delete(int id)
        {
            _recordRepository.Delete(id);
            _context.SaveChanges();
        }
        public Record Insert(Record record)
        {
            _recordsValidator.ValidateAndThrow(record);
            _recordRepository.Add(record);
            _context.SaveChanges();
            return record;
        }
        public List<Record> InsertRange(List<Record> records)
        {
            //records.ForEach(rec => _recordsValidator.ValidateAndThrow(rec));
            _recordRepository.AddRange(records);
            _context.SaveChanges();
            return records;
        }
        public Record Update(Record record)
        {
            _recordsValidator.ValidateAndThrow(record);
            _recordRepository.Edit(record);
            _context.SaveChanges();
            return record;
        }
        public List<Record> GetAll()
        {
            return _recordRepository.GetAll().ToList();
        }
        public Record GetById(int id)
        {
            return _recordRepository.GetById(id);
        }
        public IQueryable<Record> FilterByAge(int startAge = 0, int endAge = 100)
        {
            return _recordRepository.RangeByAge(startAge, endAge);
        }
        public IQueryable<Record> FilterByEmail(string startEmail, string endEmail)
        {
            return _recordRepository.RangeByEmail(startEmail, endEmail);
        }
        public IQueryable<Record> FilterByFirstName(string startName, string endName)
        {
            return _recordRepository.RangeByFirstName(startName, endName);
        }
        public IQueryable<Record> FilterByLastName(string startName, string endName)
        {
            return _recordRepository.RangeByLastName(startName, endName);
        }
        public IQueryable<Record> FilterByID(long startID = 0, long endID = long.MaxValue)
        {
            return _recordRepository.RangeByID(startID, endID);
        }
        public IQueryable<Record> FilterByIP(string startIP, string endIP)
        {
            return _recordRepository.RangeByIP(startIP, endIP);
        }
        public IQueryable<Record> FilterByTime(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            return _recordRepository.RangeByTime(startTime, endTime);
        }
        public int GetRecordsAmount()
        {
            return _recordRepository.GetAll().Count();
        }
    }
}
