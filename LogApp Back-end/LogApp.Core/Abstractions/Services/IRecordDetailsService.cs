namespace LogApp.Core.Abstractions.Services
{
    public interface IRecordDetailsService<DTO> where DTO : class
    {
        public DTO GetDetails(long id);
    }
}
