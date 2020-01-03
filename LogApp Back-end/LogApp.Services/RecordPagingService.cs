using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using LogApp.Core.Models;
using LogApp.Core.DTO;
using LogApp.Core.Abstractions;
using LogApp.Core.Abstractions.Services;

namespace LogApp.Services
{
    public class RecordPagingService : IRecordPagingService<RecordOverallDTO>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public RecordPagingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<RecordOverallDTO> GetPage(int page, int pageSize)
        {
            var records = _unitOfWork.Records.GetRange(page, pageSize).ToList();
            var recordsDTO = _mapper.Map<List<Record>, List<RecordOverallDTO>>(records);
            return recordsDTO;
        }
    }
}
