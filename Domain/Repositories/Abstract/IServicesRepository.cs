using MyCompany.Domain.Entities;

namespace MyCompany.Domain.Repositories.Abstract
{
    public interface IServicesRepository
    {
        Task<IEnumerable<Service>> GetServiceAsync();
        Task<Service?> GetServiceIdAsync(int id);
        Task SaveServiceAsync(Service entity);
        Task DeleteServiceAsync(int id);
     
    }
}
