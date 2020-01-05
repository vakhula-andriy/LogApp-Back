using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LogApp.Core.DTO;
using LogApp.Core.Abstractions.Services;

namespace LogApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly IRecordPagingService<RecordOverallDTO> _recordPagingService;
        private readonly IRecordDetailsService<RecordDetailsDTO> _recordDetailsService;
        private static int _pageSize = 25;
        private static int? _recordsAmount = null;
        public RecordController(IRecordService recordService, 
                                IRecordPagingService<RecordOverallDTO> recordPagingService,
                                IRecordDetailsService<RecordDetailsDTO> recordDetailsService)
        {
            _recordService = recordService;
            _recordPagingService = recordPagingService;
            _recordDetailsService = recordDetailsService;
        }

        [HttpGet("/recordAmount")]
        public ActionResult<int> GetRecordsAmount()
        {
            if (_recordsAmount == null)
                _recordsAmount = _recordService.GetRecordsAmount();
            return _recordsAmount;
        }

        [HttpGet]
        public ActionResult SetPageSize([FromQuery] int pageSize = 25)
        {
            _pageSize = pageSize;
            return Ok();
        }

        [HttpGet("/details/{id}")]
        public ActionResult<RecordDetailsDTO> GetDetails(long id)
        {
            return _recordDetailsService.GetDetails(id);
        }

        [HttpGet("/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetPage(int page)
        {
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/ID/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByID(int page,
                                                            [FromQuery] long minID = 1,
                                                            [FromQuery] long maxID = long.MaxValue)
        {
            _recordService.FilterByID(minID, maxID);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/age/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByAge(int page,
                                                             [FromQuery] int minAge = 0, 
                                                             [FromQuery] int maxAge = 99)
        {
            _recordService.FilterByAge(minAge, maxAge);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/firstName/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByFirstName(int page,
                                                                   [FromQuery] string minName = @"A*", 
                                                                   [FromQuery] string maxName = @"z*")
        {
            _recordService.FilterByFirstName(minName, maxName);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/lastName/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByLastName(int page,
                                                                  [FromQuery] string minName = @"A*",
                                                                  [FromQuery] string maxName = @"z*")
        {
            _recordService.FilterByLastName(minName, maxName);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/email/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByEmail(int page,
                                                               [FromQuery] string minEmail = @"\0*",
                                                               [FromQuery] string maxEmail = @"z*")
        {
            _recordService.FilterByEmail(minEmail, maxEmail);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/IP/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByIP(int page,
                                                            [FromQuery] string minIP = ".*",
                                                            [FromQuery] string maxIP = "9*")
        {
            _recordService.FilterByIP(minIP, maxIP);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/time/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByTime(int page,
                                                              [FromQuery] DateTimeOffset? minTime = null,
                                                              [FromQuery] DateTimeOffset? maxTime = null)
        {
            if (minTime == null)
                minTime = new DateTimeOffset(new DateTime(2000, 1, 1));
            if (maxTime == null)
                maxTime = new DateTimeOffset(new DateTime(3000, 12, 31));
            _recordService.FilterByTime(minTime, maxTime);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }
    }
}
