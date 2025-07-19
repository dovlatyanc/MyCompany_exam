﻿using MyCompany.Domain.Entities;

namespace MyCompany.Domain.Repositories.Abstract
{
    public interface IServiceCategoriesRepository
    {
        Task <IEnumerable<ServiceCategory>>GetServiceCategoriesAsync ();  
        Task<ServiceCategory?> GetServiceCategoryyIdAsync(int id);
        Task SaveServiceCategoryAsync (ServiceCategory entity);
        Task DeleteServiceCategoryAsync (int id);
    }
}
