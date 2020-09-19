using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using LogApp.Core.Models;
using LogApp.Core.DTO;
using LogApp.Core.Abstractions.Services;
using LogApp.Core.Abstractions.Repositories;

namespace LogApp.Services
{
    public class RecordPagingService : IRecordPagingService<RecordOverallDTO, Record>
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IMapper _mapper;
        public RecordPagingService(IRecordRepository recordRepository, IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public List<RecordOverallDTO> GetPage(int page, int pageSize)
        {
            var records = _recordRepository.GetAll();
            var rangedRecords = _recordRepository.GetRange(records, page, pageSize).ToList();
            var recordsDTO = _mapper.Map<List<Record>, List<RecordOverallDTO>>(rangedRecords);
            return recordsDTO;
        }

        public List<RecordOverallDTO> GetPage(IQueryable<Record> records, int page, int pageSize)
        {
            var rangedRecords = _recordRepository.GetRange(records, page, pageSize).ToList();
            var recordsDTO = _mapper.Map<List<Record>, List<RecordOverallDTO>>(rangedRecords);
            return recordsDTO;
        }
    }
}
