using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace productsCategories.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int ProductId {get;set;}
        public string Name{get;set;}
        [Required]
        public string Description{get;set;}
        [Required]
        public int Price {get;set;}
        public List<Shared> SharedCat {get;set;}
        public DateTime CreatedAt{get;set;}=DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}