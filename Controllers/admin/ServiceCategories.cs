using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain.Entities;

namespace MyCompany.Controllers.admin
{
    public partial class AdminController
    {
        public async Task<IActionResult> ServiceCategoriesEdit(int id)
        {
            //зависимости от наличия ID добавляем либо изменяем запись
            ServiceCategory? entity = id == default ? new ServiceCategory() : await _dataManager.ServiceCategories.GetServiceCategoryyIdAsync(id);

            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> ServiceCategoriesEdit(ServiceCategory entity)
        {
            // в модели присутствуют ошибки возыращаем на доработку
            if (!ModelState.IsValid)
                return View(entity);

            await _dataManager.ServiceCategories.SaveServiceCategoryAsync(entity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ServiceCategoriesDelete(int id)
        {
            // тк в целях безопасности отключено каскадное удаление убедитесь прежде чем удалить категори
            // что на нее нет ссылак ни у одной  из услуг
            await _dataManager.ServiceCategories.DeleteServiceCategoryAsync(id);

            return RedirectToAction("Index");
        }
    }
}
