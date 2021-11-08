using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class BascketController : Controller
    {
        private IDishRepository storage;
        private IProcessorOrder processorOrder;
        public BascketController(IDishRepository repo, IProcessorOrder processor)
        {
            storage = repo;
            processorOrder = processor;
        }

        public ViewResult Index(Bascket bascket, string returnUrl)
        {
            return View(new BascketIndexViewModel
            {
                Bascket = bascket,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToBascket(Bascket bascket, int dishID, string returnUrl)
        {
            Dish dish = storage.Dishes
                .FirstOrDefault(x => x.DishID == dishID);
            if (dish != null )
            {
                bascket.AddItem(dish, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult DeleteFromBascket(Bascket bascket, int dishID, string returnUrl)
        {
            Dish dish = storage.Dishes
                .FirstOrDefault(x => x.DishID == dishID);
            if ( dish != null )
            {
                bascket.DeleteLine(dish);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Resume(Bascket bascket)
        {
            return PartialView(bascket);
        }

        public ViewResult Checkout()
        {
            return View(new DeliveryDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Bascket bascket, DeliveryDetails deliveryDetails)
        {
            if (bascket.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                processorOrder.ProcessOrder(bascket, deliveryDetails);
                    bascket.Clear();
                return View("Completed");
            }
            else 
            return View(new DeliveryDetails());
        }
    }
}