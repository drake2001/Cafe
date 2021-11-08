using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concreate
{
    public class EDDishRepository : IDishRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Dish> Dishes
        {
            get { return context.Dishes; }
        }

        public void SaveDish(Dish dish)
        {
           if (dish.DishID == 0)
            {
                context.Dishes.Add(dish);
            }
           else
            {
                Dish dbEntry = context.Dishes.Find(dish.DishID);
                if (true)
                {
                    dbEntry.Name = dish.Name;
                    dbEntry.Author = dish.Author;
                    dbEntry.Description = dish.Description;
                    dbEntry.Type = dish.Type;
                    dbEntry.Price = dish.Price;
                }
            }
            context.SaveChanges();
        }
    }
}
