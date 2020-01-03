using AutoMapper;
using LogApp.Core.Models;
using LogApp.Core.DTO;
using LogApp.Core.Abstractions;
using LogApp.Core.Abstractions.Services;

namespace LogApp.Services
{
    public class RecordDetailsService : IRecordDetailsService<RecordDetailsDTO>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public RecordDetailsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public RecordDetailsDTO GetDetails(long id)
        {
            var record = _unitOfWork.Records.GetById(id);
            var recordDTO = _mapper.Map<Record, RecordDetailsDTO>(record);
            return recordDTO;
        }
    }
}
