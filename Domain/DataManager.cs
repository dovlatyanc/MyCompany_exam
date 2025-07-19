using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.EntityFramework;

namespace MyCompany.Domain
{
    public class DataManager
    {
        public IServiceCategoriesRepository ServiceCategories { get; set; }
        public IServicesRepository Services { get; set; }
        public DataManager( IServiceCategoriesRepository serviceCategoriesRepository,IServicesRepository servicesRepository)
        {
            Services = servicesRepository;
            ServiceCategories = serviceCategoriesRepository;
        }

    }
}
