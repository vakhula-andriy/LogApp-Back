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
        public int GetRecordsAmount()
        {
            return _unitOfWork.Records._records.Count();
        }
        public void FilterByAge(int startAge, int endAge)
        {
            _unitOfWork.Records.RangeByAge(startAge, endAge);
        }
        public void FilterByEmail(string startEmail, string endEmail)
        {
            _unitOfWork.Records.RangeByEmail(startEmail, endEmail);
        }
        public void FilterByFirstName(string startName, string endName)
        {
            _unitOfWork.Records.RangeByFirstName(startName, endName);
        }
        public void FilterByLastName(string startName, string endName)
        {
            _unitOfWork.Records.RangeByLastName(startName, endName);
        }
        public void FilterByID(long startID, long endID)
        {
            _unitOfWork.Records.RangeByID(startID, endID);
        }
        public void FilterByIP(string startIP, string endIP)
        {
            _unitOfWork.Records.RangeByIP(startIP, endIP);
        }
        public void FilterByTime(DateTimeOffset? startTime, DateTimeOffset? endTime)
        {
            _unitOfWork.Records.RangeByTime(startTime, endTime);
        }

    }
}
