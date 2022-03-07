using Microsoft.AspNetCore.Mvc;
using BaiTapWeb.Models;
using PagedList;
namespace BaiTapWeb.Controllers
{
    public class OrderController : Controller
    {
        PRODUCT_MANAGERContext _context = new PRODUCT_MANAGERContext();

        public IActionResult Index(int? page)
        {
            var orders = _context.VOrders.OrderBy(o => o.Date).ToList();


            var countOrder = orders.Count();

            var countPage = (countOrder % 10 == 0 ? countOrder / 10 : countOrder / 10 + 1);
            ViewBag.countPage = countPage;

            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.page = page;

            return View(orders.ToPagedList(pageNumber, pageSize));

        }
        public IActionResult NewsOrder(int id)
        {
            var products = _context.VProducts.ToList();

            ViewBag.idCustomer = id;

            return View(products);
        }
        public bool saveOrder(List<ItemOrder> arr, int idCustomer)
        {
            if(arr== null || idCustomer == null)
            {
                return false;
            }

            List<Product> products = new List<Product>();
            double totle = 0;
            foreach (var item in arr)
            {
                 
                totle += (item.Price * item.Quantity);
            }

            Order order = new Order();
            order.Date = DateTime.Now;
            order.Total = totle;
            order.CustomerId = idCustomer;
            _context.Orders.Add(order);

            

            _context.SaveChanges() ;
            int idOrder = order.Id;
            foreach (var item in arr)
            {
               OrderDetail orderdetail = new OrderDetail();
               orderdetail.OrderId = idOrder;
                orderdetail.ProductId = item.Id;
                orderdetail.Qty = item.Quantity;
                orderdetail.Total = item.Price * item.Quantity;
                orderdetail.Description = item.Description;
                _context.OrderDetails.Add(orderdetail);
            }
            _context.SaveChanges();


            return true;
        }

        public IActionResult DetailOrder(int id)
        {
            Order order = _context.Orders.Find(id);
            Customer customer = _context.Customers.Find(order.CustomerId);

    
         

           var products =  _context.OrderDetails.Join(_context.Products, o => o.ProductId, p => p.Id, (o, p) => new { o, p }).Where(x => x.o.OrderId == id);


            ViewBag.customer = customer;
            ViewBag.products = products;
            return View(order);
        }

        public bool DeleteOrder(int id)
        {
            if(id == null)
            {
                return false;
            }
            _context.OrderDetails.RemoveRange(_context.OrderDetails.Where(x => x.OrderId == id));
            _context.Orders.RemoveRange(_context.Orders.Where(x => x.Id == id));
            _context.SaveChanges();
            return true;
        }
    }
}
