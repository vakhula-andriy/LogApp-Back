﻿using System;
using System.Linq;
using System.Collections.Generic;
using FluentValidation;
using LogApp.Core.Abstractions.Services;
using LogApp.Core.Abstractions.Repositories;
using LogApp.Core.Models;
using LogApp.Core.Validators;
using LogApp.DAL;
using LogApp.Services.MessageHub;
using Microsoft.AspNetCore.SignalR;

namespace LogApp.Services
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRepository;
        private readonly LogAppContext _context;
        private readonly RecordValidator _recordsValidator;
        private IHubContext<NotifyHub, IMesageHubClient> _hubContext;
        public RecordService(IRecordRepository recordRepository, LogAppContext logAppContext, IHubContext<NotifyHub, IMesageHubClient> hubContext)
        {
            _recordRepository = recordRepository;
            _context = logAppContext;
            _recordsValidator = new RecordValidator();
            _hubContext = hubContext;
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
            _hubContext.Clients.All.RecordsAdded(records.Count);
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
        public IQueryable<Record> FilterByEmail(string email)
        {
            return _recordRepository.RangeByEmail(email);
        }
        public IQueryable<Record> FilterByFirstName(string name)
        {
            return _recordRepository.RangeByFirstName(name);
        }
        public IQueryable<Record> FilterByLastName(string name)
        {
            return _recordRepository.RangeByLastName(name);
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
