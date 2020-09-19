using System;
using System.Linq;
using System.Collections.Generic;
using FluentValidation;
using LogApp.Core.Abstractions;
using LogApp.Core.Abstractions.Services;
using LogApp.Core.Models;
using LogApp.Core.Validators;

namespace LogApp.Services
{
    public class RecordService : IRecordService
    {
        private IUnitOfWork _unitOfWork;
        private readonly RecordValidator _recordsValidator;
        public RecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _recordsValidator = new RecordValidator();
        }

        public void Delete(int id)
        {
            _unitOfWork.Records.Delete(id);
            _unitOfWork.Save();
        }
        public Record Insert(Record record)
        {
            _recordsValidator.ValidateAndThrow(record);
            _unitOfWork.Records.Add(record);
            _unitOfWork.Save();
            return record;
        }
        public List<Record> InsertRange(List<Record> records)
        {
            //records.ForEach(rec => _recordsValidator.ValidateAndThrow(rec));
            _unitOfWork.Records.AddRange(records);
            _unitOfWork.Save();
            return records;
        }
        public Record Update(Record record)
        {
            _recordsValidator.ValidateAndThrow(record);
            _unitOfWork.Records.Edit(record);
            _unitOfWork.Save();
            return record;
        }
        public List<Record> GetAll()
        {
            return _unitOfWork.Records.GetAll().ToList();
        }
        public Record GetById(int id)
        {
            return _unitOfWork.Records.GetById(id);
        }
        public IQueryable<Record> FilterByAge(int startAge = 0, int endAge = 100)
        {
            return _unitOfWork.Records.RangeByAge(startAge, endAge);
        }
        public IQueryable<Record> FilterByEmail(string startEmail, string endEmail)
        {
            return _unitOfWork.Records.RangeByEmail(startEmail, endEmail);
        }
        public IQueryable<Record> FilterByFirstName(string startName, string endName)
        {
            return _unitOfWork.Records.RangeByFirstName(startName, endName);
        }
        public IQueryable<Record> FilterByLastName(string startName, string endName)
        {
            return _unitOfWork.Records.RangeByLastName(startName, endName);
        }
        public IQueryable<Record> FilterByID(long startID = 0, long endID = long.MaxValue)
        {
            return _unitOfWork.Records.RangeByID(startID, endID);
        }
        public IQueryable<Record> FilterByIP(string startIP, string endIP)
        {
            return _unitOfWork.Records.RangeByIP(startIP, endIP);
        }
        public IQueryable<Record> FilterByTime(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            return _unitOfWork.Records.RangeByTime(startTime, endTime);
        }
        public int GetRecordsAmount()
        {
            return _unitOfWork.Records.GetAll().Count();
        }
    }
}
