
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Dish
    {
        [HiddenInput(DisplayValue=false)]
        [Display(Name="ID")]
        public int DishID { get; set; }

        [Display(Name="Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название блюда")]
        public string Name { get; set; }

        [Display(Name="Автор")]
        [Required(ErrorMessage = "Пожалуйста, укажите кухню")]
        public string Author { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Пожалуйста, укажите описание для блюда")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Тип")]
        [Required(ErrorMessage = "Пожалуйста, укажите тип блюда")]
        public string Type { get; set; }

        [Display(Name = "Цена (руб)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public decimal Price { get; set; }

    }
}
