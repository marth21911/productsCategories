using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace productsCategories.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get;set;}
        [Required]
        public string Name {get;set;}
        public List<Shared> SharedProd {get;set;}
        public DateTime CreatedAt{get;set;}=DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}