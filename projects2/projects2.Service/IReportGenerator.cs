using System.Threading.Tasks;

namespace SchedulingConfigurationTool
{
    public interface IReportGenerator
    {
        Task GenerateReport(FileType fileType);
    }

    public interface IReportDeliveryConfiguration
    {
        DestinationType DestinationType { get; set; }
        string DestinationAddress { get; set; }
        bool ValidateDestination();
        bool ValidateDeliveryConfiguration();
    }

    public interface ISchedulingConfigurationToolService
    {
        Task Run();
    }
}