using LogApp.Core.Abstractions.Services;
using LogApp.Core.DTO;
using LogApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly IRecordPagingService<RecordOverallDTO, Record> _recordPagingService;
        private readonly IRecordDetailsService<RecordDetailsDTO> _recordDetailsService;

        public RecordController(IRecordService recordService,
                                IRecordPagingService<RecordOverallDTO, Record> recordPagingService,
                                IRecordDetailsService<RecordDetailsDTO> recordDetailsService)
        {
            _recordService = recordService;
            _recordPagingService = recordPagingService;
            _recordDetailsService = recordDetailsService;
        }

        [HttpGet("/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetPage(int page, int pageSize = 25)
        {
            Response.Headers.Add("records_amount", _recordService.GetRecordsAmount().ToString());
            return _recordPagingService.GetPage(page, pageSize);
        }

        [HttpGet("/{field}/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByID(int page,
                                                            string field,
                                                            [FromQuery] string searchValue,
                                                            [FromQuery] string minFilterValue,
                                                            [FromQuery] string maxFilterValue,
                                                            [FromQuery] int pageSize = 25)
        {
            IQueryable<Record> filteredRecords;
            switch (field)
            {
                case "ID":
                    filteredRecords = _recordService.FilterByID(long.Parse(minFilterValue), long.Parse(maxFilterValue));
                    break;
                case "age":
                    filteredRecords = _recordService.FilterByAge(int.Parse(minFilterValue), int.Parse(maxFilterValue));
                    break;
                case "IP":
                    filteredRecords = _recordService.FilterByIP(minFilterValue, maxFilterValue);
                    break;
                case "time":
                    filteredRecords = _recordService.FilterByTime(DateTimeOffset.Parse(minFilterValue), DateTimeOffset.Parse(maxFilterValue));
                    break;
                case "firstName":
                    filteredRecords = _recordService.FilterByFirstName(searchValue);
                    break;
                case "lastName":
                    filteredRecords = _recordService.FilterByLastName(searchValue);
                    break;
                case "email":
                    filteredRecords = _recordService.FilterByEmail(searchValue);
                    break;
                default:
                    throw new ArgumentException($"Filtering by field {field} is not supported. Try another one or fix the typo");
            }

            Response.Headers.Add("Records_Amount", filteredRecords.Count().ToString());
            return _recordPagingService.GetPage(filteredRecords, page, pageSize);
        }

        [HttpGet("/details/{id}")]
        public ActionResult<RecordDetailsDTO> GetDetails(long id)
        {
            return _recordDetailsService.GetDetails(id);
        }
    }
}
