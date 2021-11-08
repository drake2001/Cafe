using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public   class DeliveryDetails
    {
        [Required(ErrorMessage = "Укажите Ваше имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Адрес доставки")]
        [Display(Name="Первый адрес")]
        public string Address1 { get; set; }
        [Display(Name = "Второй адрес")]
        public string Address2 { get; set; }
        [Display(Name = "Третий адрес")]
        public string Address3 { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }


        public bool GiftPackaging { get; set; }
    }
}
