﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using System.Text.Json;

namespace MyCompany.Controllers.admin
{
    [Authorize(Roles ="admin")]
    public partial class AdminController:Controller
    {
        private readonly DataManager _dataManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        public AdminController(DataManager dataManager, IWebHostEnvironment webHostEnvironment)
        {
            _dataManager = dataManager;
            _hostingEnvironment = webHostEnvironment;
        }

        public async Task <IActionResult> Index()
        {
            ViewBag.ServiceCategories = await _dataManager.ServiceCategories.GetServiceCategoriesAsync();
            ViewBag.Services =await _dataManager.Services.GetServiceAsync();
            return View();
        }
        //сохраняем картинку в файловую систему
        public async Task<string> SaveImg(IFormFile img) 
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "img/", img.FileName);
            await using FileStream stream = new FileStream(path, FileMode.Create);
            await img.CopyToAsync(stream);

            return path;
        }
        //сохраняем картинку из редактора
        public async Task<string> SaveEditorImg() {

            IFormFile img = Request.Form.Files[0];
            await SaveImg(img);

            return JsonSerializer.Serialize(new { location = Path.Combine("/img/", img.FileName) });
        }
    }
}
