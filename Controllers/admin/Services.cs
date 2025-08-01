﻿using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace MyCompany.Controllers.admin
{
    public partial class AdminController
    {
        public async Task<IActionResult> ServicesEdit(int id)
        {
            //зависимости от наличия ID добавляем либо изменяем запись
            Service? entity = id == default ? new Service() : await _dataManager.Services.GetServiceIdAsync(id);
            ViewBag.ServiceCategories = await _dataManager.ServiceCategories.GetServiceCategoriesAsync();

            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> ServicesEdit(Service entity, IFormFile? titleImageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ServiceCategories = await _dataManager.ServiceCategories.GetServiceCategoriesAsync();
                return View(entity);    
            }
            if (titleImageFile != null) 
            {
                entity.Photo =titleImageFile.FileName;
                await SaveImg(titleImageFile);
            }

            await _dataManager.Services.SaveServiceAsync(entity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ServicesDelete(int id)
        {
            
            await _dataManager.Services.DeleteServiceAsync(id);

            return RedirectToAction("Index");
        }
    }
}
