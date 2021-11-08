using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IDishRepository> mock = new Mock<IDishRepository>();
            mock.Setup(m => m.Dishes).Returns(new List<Dish>
                {
                new Dish { DishID = 1, Name = "Dish1"},
                new Dish { DishID = 2, Name = "Dish2" },
                new Dish { DishID = 3, Name = "Dish3" },
                new Dish { DishID = 4, Name = "Dish4" },
                new Dish { DishID = 5, Name = "Dish5" }
            });

            DishesController controller = new DishesController(mock.Object);
            controller.pageSize = 3;
            IEnumerable<Dish> result = (IEnumerable<Dish>)controller.List(2).Model;
            List<Dish> dishes = result.ToList();
            Assert.IsTrue(dishes.Count == 2);
            Assert.AreEqual(dishes[0].Name, "Dish4");
            Assert.AreEqual(dishes[1].Name, "Dish5");

            [TestMethod]
            public void Can_Generate_Page_Links()
            {
                //Организация
                HtmlHelper myHelper = null;
                PagingInfo pagingInfo = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                };

                Func<int, string> pageUrlDelegate = i => "Page" + i;
                //Действие
                MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

                //Утверждение
                Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page2"">1</a>"
                  + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2/a>"
                  + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                  result.ToString());
            }
        }
    }
}
