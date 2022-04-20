using System;
using System.ComponentModel.DataAnnotations;
namespace productsCategories.Models
{
    public class Shared
    {
        [Key]
        public int SharedId {get;set;}
        public int ProductId {get;set;}
        public int CategoryId{get;set;}
        public Product Product{get;set;}
        public Category Category{get;set;}
    }
}