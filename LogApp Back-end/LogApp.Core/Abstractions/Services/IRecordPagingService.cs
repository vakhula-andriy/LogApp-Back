using System.Linq;
using System.Collections.Generic;

namespace LogApp.Core.Abstractions.Services
{
    public interface IRecordPagingService<DTO, ENT>
    {
        public List<DTO> GetPage(int page, int pageSize);
        public List<DTO> GetPage(IQueryable<ENT> records, int page, int pageSize);
    }
}
