using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;

namespace MyCompany.Models.Components
{
    public class MenuViewComponent:ViewComponent
    {
        private readonly DataManager _dataManager;

        public MenuViewComponent(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        private async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Service> list = await _dataManager.Services.GetServiceAsync();
            return await Task.FromResult((IViewComponentResult) View("Default",list));
        }
    }
}
