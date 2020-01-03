using Bogus;
using LogApp.Core.Models;
using LogApp.Core.Abstractions.Services;

namespace LogApp.Core
{
    public class RecordFiller
    {
        private IRecordService _recordService;
        public RecordFiller(IRecordService recordService)
        {
            _recordService = recordService;
        }

        public void RandomFill()
        {
            var recordFake = new Faker<Record>()
                .RuleFor(rec => rec.FirstName, f => f.Name.FirstName())
                .RuleFor(rec => rec.LastName, f => f.Name.LastName())
                .RuleFor(rec => rec.Email, (f, rec) => f.Internet.Email(rec.FirstName, rec.LastName))
                .RuleFor(rec => rec.Age, f => f.Random.Int(18, 50))
                .RuleFor(rec => rec.IPAdress, f => $"{f.Random.Number(255)}." +
                                                   $"{f.Random.Number(255)}." +
                                                   $"{f.Random.Number(255)}." +
                                                   $"{f.Random.Number(255)}")
                .RuleFor(rec => rec.Timestamp, f => f.Date.PastOffset());

            var records = recordFake.Generate(5);
            _recordService.InsertRange(records);
        }
    }
}
