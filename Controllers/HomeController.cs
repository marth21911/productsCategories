using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using productsCategories.Models;
using Microsoft.EntityFrameworkCore;

namespace productsCategories.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet("/category/{CategoryId}")]
        public IActionResult OneCategory(int CategoryId)
        {
            ViewBag.OneCategory = 
                _context.Categories.
                Include(a=>a.SharedProd)
                .ThenInclude(a=>a.Product).
                FirstOrDefault(a=>a.CategoryId == CategoryId);
            // ViewBag.AllProducts = 
            //     _context.Products.
            //     OrderBy(a => a.Name).ToList();
            ViewBag.RemainingProd= 
                _context.Products.
                Include(p=>p.SharedCat).
                Where(o=>o.SharedCat.All(v=>v.CategoryId != CategoryId));
            return View();
        }
        [HttpPost("addShared")]
        public IActionResult AddShared(Shared newShare, string option)
        {
            _context.Shareds.Add(newShare);
            Console.WriteLine(newShare.ProductId);
            Console.WriteLine(newShare.CategoryId);
            _context.SaveChanges();
            if(option == "product")
            {
            return Redirect($"/product/{newShare.ProductId}");
            } else{
                return Redirect($"/category/{newShare.CategoryId}");
            }
        }

        [HttpGet("/product/{ProductId}")]
        public IActionResult OneProduct(int ProductID)
        {
            ViewBag.OneProduct = 
                _context.Products.
                Include(a=>a.SharedCat)
                .ThenInclude(a=>a.Category).
                FirstOrDefault(a=>a.ProductId == ProductID);
            ViewBag.RemainingCat = 
                _context.Categories.
                Include(a=>a.SharedProd).
                Where(b=>b.SharedProd.All(c=>c.ProductId != ProductID));
            return View ();
        }
        [HttpGet("/productmaker")]
        public IActionResult NewProduct()
        {
            ViewBag.AllProducts = 
            _context.Products.
            OrderBy(a => a.Name).ToList();
        return View();
        }
        [HttpPost("/product/create")]
        public IActionResult MakeProduct(Product newProduct)
        {
            if(ModelState.IsValid)
            {
                _context.Products.Add(newProduct);
                _context.SaveChanges();
                return RedirectToAction ("NewProduct");
            }else{
                ViewBag.AllProducts = _context.Products.OrderBy(a => a.Name).ToList();
                return View("NewProduct");
            }

        }
        [HttpPost("/category/create")]
        public IActionResult MakeCategory(Category newCategory)
        {
            if(ModelState.IsValid)
            {
                _context.Categories.Add(newCategory);
                _context.SaveChanges();
                return RedirectToAction ("NewCategory");
            }else{
                ViewBag.AllCategories=_context.Categories.OrderBy(a=>a.Name).ToList();
                return View("NewCategory");
            }

        }
        [HttpGet("/categorymaker")]
        public IActionResult NewCategory()
        {
            ViewBag.AllCategories=_context.Categories.OrderBy(a=>a.Name).ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
