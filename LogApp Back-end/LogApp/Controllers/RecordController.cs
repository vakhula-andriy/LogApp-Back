using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LogApp.Core.DTO;
using LogApp.Core.Models;
using LogApp.Core.Abstractions.Services;

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

        [HttpGet("/details/{id}")]
        public ActionResult<RecordDetailsDTO> GetDetails(long id)
        {
            return _recordDetailsService.GetDetails(id);
        }

        [HttpGet("/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetPage(int page, int pageSize = 25)
        {
            Response.Headers.Add("Records_Amount", _recordService.GetRecordsAmount().ToString());
            return _recordPagingService.GetPage(page, pageSize);
        }

        [HttpGet("/ID/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByID([FromQuery] long minID,
                                                            [FromQuery] long maxID,
                                                            int page,
                                                            int pageSize = 25)
        {
            var filteredRecords = _recordService.FilterByID(minID, maxID);
            Response.Headers.Add("Records_Amount", filteredRecords.Count().ToString());
            return _recordPagingService.GetPage(filteredRecords, page, pageSize);
        }

        [HttpGet("/age/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByAge([FromQuery] int minAge,
                                                             [FromQuery] int maxAge,
                                                             int page,
                                                             int pageSize = 25)
        {
            var filteredRecords = _recordService.FilterByAge(minAge, maxAge);
            Response.Headers.Add("Records_Amount", filteredRecords.Count().ToString());
            return _recordPagingService.GetPage(filteredRecords, page, pageSize);
        }

        [HttpGet("/firstName/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByFirstName([FromQuery] string minName,
                                                                   [FromQuery] string maxName,
                                                                   int page,
                                                                   int pageSize = 25)
        {
            var filteredRecords = _recordService.FilterByFirstName(minName, maxName);
            Response.Headers.Add("Records_Amount", filteredRecords.Count().ToString());
            return _recordPagingService.GetPage(filteredRecords, page, pageSize);
        }

        [HttpGet("/lastName/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByLastName([FromQuery] string minName,
                                                                  [FromQuery] string maxName,
                                                                  int page,
                                                                  int pageSize = 25)
        {
            var filteredRecords = _recordService.FilterByLastName(minName, maxName);
            Response.Headers.Add("Records_Amount", filteredRecords.Count().ToString());
            return _recordPagingService.GetPage(filteredRecords, page, pageSize);
        }

        [HttpGet("/email/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByEmail([FromQuery] string minEmail,
                                                               [FromQuery] string maxEmail,
                                                               int page,
                                                               int pageSize = 25)
        {
            var filteredRecords = _recordService.FilterByEmail(minEmail, maxEmail);
            Response.Headers.Add("Records_Amount", filteredRecords.Count().ToString());
            return _recordPagingService.GetPage(filteredRecords, page, pageSize);
        }

        [HttpGet("/IP/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByIP([FromQuery] string minIP,
                                                            [FromQuery] string maxIP,
                                                            int page,
                                                            int pageSize = 25)
        {
            var filteredRecords = _recordService.FilterByIP(minIP, maxIP);
            Response.Headers.Add("Records_Amount", filteredRecords.Count().ToString());
            return _recordPagingService.GetPage(filteredRecords, page, pageSize);
        }

        [HttpGet("/time/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByTime([FromQuery] DateTimeOffset minTime,
                                                              [FromQuery] DateTimeOffset maxTime,
                                                              int page,
                                                              int pageSize = 25)
        {
            var filteredRecords = _recordService.FilterByTime(minTime, maxTime);
            Response.Headers.Add("Records_Amount", filteredRecords.Count().ToString());
            return _recordPagingService.GetPage(filteredRecords, page, pageSize);
        }
    }
}
