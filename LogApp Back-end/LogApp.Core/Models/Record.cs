using System;

namespace LogApp.Core.Models
{
    public class Record : IEntity<long>
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string IPAdress { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
