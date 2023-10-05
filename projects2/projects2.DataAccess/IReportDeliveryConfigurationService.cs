


using System.Collections.Generic;
using System.Threading.Tasks;

namespace projects2.Service
{
    public interface IReportDeliveryConfigurationService
    {
        Task<int> AddAsync(ReportDeliveryConfigurationModel model);
        Task<ReportDeliveryConfigurationModel> GetByIdAsync(int id);
        Task<List<ReportDeliveryConfigurationModel>> GetAllAsync();
        Task UpdateAsync(int id, ReportDeliveryConfigurationModel model);
        Task DeleteAsync(int id);
    }
}
