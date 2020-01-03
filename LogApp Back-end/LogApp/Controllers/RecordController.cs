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
        private static int _pageSize = 3;
        private static int _recordsAmount = 0;
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
            if (_recordsAmount == 0)
                _recordsAmount = _recordService.GetRecordsAmount();
            return _recordsAmount;
        }

        [HttpGet]
        public ActionResult SetPageSize([FromQuery] int pageSize)
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
        public ActionResult<List<RecordOverallDTO>> GetByID([FromQuery] long minID,
                                                            [FromQuery] long maxID, int page)
        {
            _recordService.FilterByID(minID, maxID);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/age/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByAge([FromQuery] int minAge, 
                                                             [FromQuery] int maxAge, int page)
        {
            _recordService.FilterByAge(minAge, maxAge);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/firstName/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByFirstName([FromQuery] string minName, 
                                                                   [FromQuery] string maxName, int page)
        {
            _recordService.FilterByFirstName(minName, maxName);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/lastName/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByLastName([FromQuery] string minName, 
                                                                  [FromQuery] string maxName, int page)
        {
            _recordService.FilterByLastName(minName, maxName);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/email/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByEmail([FromQuery] string minEmail,
                                                               [FromQuery] string maxEmail, int page)
        {
            _recordService.FilterByEmail(minEmail, maxEmail);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/IP/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByIP([FromQuery] string minIP,
                                                            [FromQuery] string maxIP, int page)
        {
            _recordService.FilterByIP(minIP, maxIP);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }

        [HttpGet("/time/{page}")]
        public ActionResult<List<RecordOverallDTO>> GetByTime([FromQuery] DateTimeOffset minTime,
                                                              [FromQuery] DateTimeOffset maxTime, int page)
        {
            _recordService.FilterByTime(minTime, maxTime);
            _recordsAmount = _recordService.GetRecordsAmount();
            return _recordPagingService.GetPage(page, _pageSize);
        }
    }
}
