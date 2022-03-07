using Microsoft.AspNetCore.Mvc;
using BaiTapWeb.Models;
using PagedList;
namespace BaiTapWeb.Controllers
{
    public class ProductController : Controller
    {
        PRODUCT_MANAGERContext _context = new PRODUCT_MANAGERContext();

        public IActionResult Index(int? page,int idCategory = -1)
        {
            var products = _context.VProducts.ToList();

            if( idCategory != -1)
            {
                products = _context.VProducts.Where(p => p.CategoryId == idCategory).ToList();
            }
        

            var countProduct = products.Count();

            var countPage = (countProduct % 10 == 0 ? countProduct / 10 : countProduct / 10 + 1);
            var categorys = _context.Categories.ToList();
            ViewBag.categorys = categorys;
            ViewBag.countPage = countPage;
            ViewBag.idCategory = idCategory;

            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.page = page;

            return View(products.ToPagedList(pageNumber,pageSize));
        }

        public IActionResult SearchProduct(String data)
        {
            var products = _context.VProducts.Where(p => p.Code.Contains(data)||p.Name.Contains(data)).ToList();

            return Json(products);
        }

        public IActionResult CreateProduct()
        {
            return View(_context.Categories.ToList());
        }
        public bool addProduct(string code,string name,float price,int category,string discription)
        {
            if (_context.Products.Where(x => x.Code == code).ToList().Count() > 0)
            {
                return false;
            }

            Product product = new Product();
            product.Code = code;
            product.Name = name;
            product.Price = price;
            product.Description = discription;
            product.CategoryId = category;

            _context.Products.Add(product);
            _context.SaveChanges();

            return true;
        }
        public IActionResult EditProduct(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(_context.Products.Find(id));
        }
        public bool EditProductResult(int id,string name,float price,string description,int idCategory)
        {
            Product product = _context.Products.Find(id);
            if(product == null)
            {
                return false;
            }
            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.CategoryId = idCategory;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteProduct(int id)
        {
            if(id== null)
            {
                return false;
            }
            _context.OrderDetails.RemoveRange(_context.OrderDetails.Where(x => x.ProductId == id));
            _context.Products.RemoveRange(_context.Products.Where(x => x.Id == id));
            _context.SaveChanges();

            return true;
        }
    }
}
