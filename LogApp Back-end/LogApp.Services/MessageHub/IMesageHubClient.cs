using System.Threading.Tasks;

namespace LogApp.Services.MessageHub
{
    public interface IMesageHubClient
    {
        Task RecordsAdded(int amount);
    }
}
