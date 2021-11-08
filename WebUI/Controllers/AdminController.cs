using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        IDishRepository repository;
        public AdminController(IDishRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View(repository.Dishes);
        }
        public ViewResult Edit(int dishID)
        {
            Dish dish = repository.Dishes.FirstOrDefault(b => b.DishID == dishID);
            return View(dish);
        }
      
        [HttpPost]
        public ActionResult Edit(Dish dish)
        {
            if (ModelState.IsValid)
            {
                repository.SaveDish(dish);
                TempData["message"] = string.Format("Изменение информации о блюде \"{0}\" сохранены", dish.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(dish);
            }
        }
    }
}