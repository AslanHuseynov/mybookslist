using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mybookslist.Data;
using mybookslist.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace mybookslist.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private mybookslistDbContext _db;

        public HomeController(mybookslistDbContext db)
        {
            _db = db;
        }


        public IActionResult Index(int? page)
        {
            return View(_db.BookInfo);
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

        //GET product detail acation method

        public ActionResult Detail(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var book = _db.BookInfo;
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //POST product detail acation method
        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {
            List<BookInfo> products = new List<BookInfo>();
            if (id == null)
            {
                return NotFound();
            }

            var book = _db.BookInfo;
            if (book == null)
            {
                return NotFound();
            }

            book = HttpContext.Session.Get<List<BookInfo>>("books");
            if (book == null)
            {
                book = new List<BookInfo>();
            }
            book.Add(book);
            HttpContext.Session.Set("books", book);
            return RedirectToAction(nameof(Index));
        }
        //GET Remove action methdo
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<BookInfo> book = HttpContext.Session.Get<List<BookInfo>>("books");
            if (book != null)
            {
                var book = book.FirstOrDefault(c => c.Id == id);
                if (book != null)
                {
                    book.Remove(book);
                    HttpContext.Session.Set("books", book);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public IActionResult Remove(int? id)
        {
            List<BookInfo> products = HttpContext.Session.Get<List<BookInfo>>("products");
            if (book != null)
            {
                var book = products.FirstOrDefault(c => c.Id == id);
                if (book != null)
                {
                    products.Remove(book);
                    HttpContext.Session.Set("books", book);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //GET product Cart action method

        public IActionResult Cart()
        {
            List<BookInfo> products = HttpContext.Session.Get<List<BookInfo>>("products");
            if (book == null)
            {
                book = new List<BookInfo>();
            }
            return View(book);
        }

    }

}
