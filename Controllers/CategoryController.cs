using Microsoft.AspNetCore.Mvc;
using BaiTapWeb.Models;
namespace BaiTapWeb.Controllers
{
    public class CategoryController : Controller
    {
        PRODUCT_MANAGERContext _context = new PRODUCT_MANAGERContext();

        public IActionResult Index()
        {


            return View(_context.Categories.ToList());
        }
        public bool createCategory(string code, string name, string decription)
        {
            if (_context.Categories.Where(x => x.Code == code).ToList().Count() > 0)
            {
                return false;
            }
            Category category = new Category();
            category.Code = code;
            category.Name = name;
            category.Description = decription;

            _context.Categories.Add(category);
            _context.SaveChanges();
            return true;
        }
        public bool updateCategory(string code, string name, string decription,int id)
        {
            Category category = _context.Categories.Find(id);


            if (category == null)
            {
                return false;
            }
            category.Name = name;
            category.Description = decription;
            _context.SaveChanges();

            return true;
        }
        public bool DeleteCategory(int id)
        {
            if(id == null)
            {
                return false;
            }
            Category category = _context.Categories.Find(id);
            Category categoryDefal = _context.Categories.Where(x => x.Code.Equals("d755d326-57a7-41fb-bb9a-841ee4f23b02")).First();

            var listProduct = _context.Products.Where(x => x.CategoryId == category.Id);
            for (int i = 0; i < listProduct.ToList().Count; i++)
            {
                listProduct.ToList()[i].CategoryId = categoryDefal.Id;
            }
            _context.SaveChanges();

            _context.Categories.Remove(category);
            _context.SaveChanges();


            return true;
        }
    }
}
