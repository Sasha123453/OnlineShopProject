using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.Models
{
    public class OrderInfoModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
