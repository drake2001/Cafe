using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class Bascket
    {
        private List<BascketLine> lineCollection = new List<BascketLine>();

        public IEnumerable<BascketLine> Lines { get { return lineCollection; } }

        public void AddItem(Dish dish, int volume)
        {
            BascketLine line = lineCollection
                .Where(x => x.Dish.DishID == dish.DishID)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new BascketLine { Dish = dish, Quantity = volume });
            }
            else
            {
                line.Quantity += volume;
            }
        }
        public void DeleteLine(Dish dish)
        {
            lineCollection.RemoveAll(l => l.Dish.DishID == dish.DishID);
        }

        public decimal CalculateTotalValue()
        {
            return lineCollection.Sum(e => e.Dish.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }
    public class BascketLine
    {
        public Dish Dish { get; set; }
        public int Quantity { get; set; }
    }
}
