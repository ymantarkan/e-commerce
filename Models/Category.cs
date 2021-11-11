using System.ComponentModel.DataAnnotations;

namespace urun_katalog.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Ürün Adı")]
        public string ProductName { get; set; }
    }
}