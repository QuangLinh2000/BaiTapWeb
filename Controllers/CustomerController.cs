using Microsoft.AspNetCore.Mvc;
using BaiTapWeb.Models;
using Microsoft.AspNetCore.Session;
namespace BaiTapWeb.Controllers
{
    public class CustomerController : Controller
    {
        PRODUCT_MANAGERContext _context = new PRODUCT_MANAGERContext();

        public IActionResult Index()
        {


            return View(_context.Customers.ToList());
        }
        public bool createCustomer(String code,String name)
        {
            if(_context.Customers.Where(x => x.Code == code).ToList().Count() > 0)
            {
                return false;
            }
            Customer customer = new Customer();
            customer.Code = code;
            customer.Name = name;

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return true;
        }
        public bool updateCustomer(String code, String name,int id)
        {
            Customer customer = _context.Customers.Find(id);
            

            if (customer == null)
            {
                return false;
            }
            customer.Name = name;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCustomer(int id)
        {
            if(id == null)
            {
                return false;
            }
            var orders =  _context.Orders.Where(x => x.CustomerId == id);
            if (orders != null)
            {
                for(int i = 0; i < orders.ToList().Count; i++)
                {
                    var item = orders.ToList()[i];
                    _context.OrderDetails.RemoveRange(_context.OrderDetails.Where(x => x.OrderId == item.Id));
                }

                _context.Orders.RemoveRange(orders);


            }
            _context.Customers.RemoveRange(_context.Customers.Where(x => x.CustomerId == id));
            _context.SaveChanges();
            return true;
        }


    }

}
