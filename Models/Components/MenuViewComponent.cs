﻿using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Infrastructure;

namespace MyCompany.Models.Components
{
    public class MenuViewComponent:ViewComponent
    {
        private readonly DataManager _dataManager;

        public MenuViewComponent(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Service> list = await _dataManager.Services.GetServiceAsync();

            IEnumerable<ServiceDTO> listDTO = HelperDTO.TransformServices(list);

            return await Task.FromResult((IViewComponentResult) View("Default",listDTO));
        }
    }
}
