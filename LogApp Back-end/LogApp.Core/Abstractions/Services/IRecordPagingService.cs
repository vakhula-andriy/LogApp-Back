using System.Collections.Generic;

namespace LogApp.Core.Abstractions.Services
{
    public interface IRecordPagingService<DTO> where DTO : class
    {
        public List<DTO> GetPage(int page, int pageSize);
    }
}
