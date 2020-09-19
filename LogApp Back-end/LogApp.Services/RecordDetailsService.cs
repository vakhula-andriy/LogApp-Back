using AutoMapper;
using LogApp.Core.Models;
using LogApp.Core.DTO;
using LogApp.Core.Abstractions.Services;
using LogApp.Core.Abstractions.Repositories;

namespace LogApp.Services
{
    public class RecordDetailsService : IRecordDetailsService<RecordDetailsDTO>
    {
        private IMapper _mapper;
        private IRecordRepository _recordRepository;
        public RecordDetailsService(IRecordRepository recordRepository, IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public RecordDetailsDTO GetDetails(long id)
        {
            var record = _recordRepository.GetById(id);
            var recordDTO = _mapper.Map<Record, RecordDetailsDTO>(record);
            return recordDTO;
        }
    }
}
