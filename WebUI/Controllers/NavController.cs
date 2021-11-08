using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IDishRepository repository;

        public NavController(IDishRepository repo)
            {
            repository = repo;
            }

       public PartialViewResult Menu(string type = null, bool Navhorizontal = false)
        {
            ViewBag.SelectedType = type;

            IEnumerable<string> types = repository.Dishes
                 .Select(dish => dish.Type)
                 .Distinct()
                 .OrderBy(x => x);
            string Nameview = Navhorizontal ? "HorizontalMenu" : "Menu";
            return PartialView(Nameview,types);
        }
    }
}