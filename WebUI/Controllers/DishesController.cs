using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class DishesController : Controller
    {
        // GET: Dishes
        private IDishRepository repository;
        public int pageSize = 4;
        public DishesController(IDishRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(string type, int page = 1)
        {
            DishesListViewModel model = new DishesListViewModel
            {
                Dishes = repository.Dishes
                .Where(b => type == null || b.Type == type)
                .OrderBy(dish => dish.DishID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = type == null ?
                    repository.Dishes.Count() :
                    repository.Dishes.Where(dish => dish.Type == type).Count()
                },
                CurrentType = type
            };
            return View(model);
        }
    }
}