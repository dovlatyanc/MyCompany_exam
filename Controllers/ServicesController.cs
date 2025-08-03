﻿using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Infrastructure;
using MyCompany.Models;

namespace MyCompany.Controllersl;

public class ServicesController:Controller
{

    private readonly DataManager _dataManager;

    public ServicesController(DataManager dataManager)
    {
        _dataManager = dataManager;
    }

    public async Task <IActionResult> Index()
    {
        IEnumerable<Service> list = await _dataManager.Services.GetServiceAsync();

        IEnumerable<ServiceDTO> listDTO = HelperDTO.TransformServices(list);

       
        return View(listDTO);
    }
    public async Task<IActionResult> Show(int id)
    {

        Service? entity = await _dataManager.Services.GetServiceIdAsync(id);
       
        if(entity is null)
        {
            return NotFound();
        }

        ServiceDTO  entityDTO = HelperDTO.TransformService(entity);


        return View(entityDTO);
    }
}
