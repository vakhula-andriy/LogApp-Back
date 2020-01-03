using System;

namespace LogApp.Core.DTO
{
    public class RecordDetailsDTO
    {
        //public string LastName { get; set; }
        //public string IPAdress { get; set; }
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
