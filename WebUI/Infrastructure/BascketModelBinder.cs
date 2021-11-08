using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Infrastructure
{
    public class BascketModelBinder : IModelBinder
    {
        private const string sessionKey = "Bascket";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Bascket bascket = null;
            if (controllerContext.HttpContext.Session != null)
            {
                bascket = (Bascket)controllerContext.HttpContext.Session[sessionKey];
            }

            if (bascket == null)
            {
                bascket = new Bascket();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = bascket;
                }
            }
            return bascket;
        }
    }
}