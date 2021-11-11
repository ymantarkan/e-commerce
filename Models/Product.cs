using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace urun_katalog.Models
{
    public class Product
    {
        public int Id { get; set; }


        [Required]
        [Display(Name ="Ürün Adı")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Fiyatı")]
        public decimal Price { get; set; }

        public string Image{get;set;}

        [Display(Name ="Ürün Rengi")]
        public string ProductColor{get;set;}


         [Required]
        [Display(Name ="Ürün Mevcut mu?")]
        public bool IsAvailable { get; set; }



        [Required]
        [Display(Name ="Ürün Tipi")]
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public virtual Category Category{get;set;}



        [Required]
        [Display(Name ="Özel Etiket")]
        public int SpecialTagId { get; set; }
        [ForeignKey("SpecialTagId")]
        public virtual SpecialTag SpecialTag{get;set;}



        

        




        
    }
}