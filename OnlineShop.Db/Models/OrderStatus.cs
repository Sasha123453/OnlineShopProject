using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.Models
{
    public enum OrderStatus
    {
        [Display(Name = "Создан")]
        Created,
        [Display(Name = "Подтвержден")]
        Confirmed,
        [Display(Name = "Отправлен")]
        Shipped,
        [Display(Name = "Доставлен")]
        Delivered,
        [Display(Name = "Провален")]
        Failed,
        [Display(Name = "Отменен")]
        Cancelled
    }
}
