using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mybookslist.Data;
using mybookslist.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mybookslist.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookInfoController : Controller
    {
        private mybookslistDbContext _db;
        private IHostingEnvironment _he;

        public BookInfoController(mybookslistDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }

        public IActionResult Index()
        {
            return View(_db.BookInfo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookInfo book, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _db.BookInfo.FirstOrDefault(c => c.Name == book.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    return View(book);
                }

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    book.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    book.Image = "Images/noimage.PNG";
                }
                _db.BookInfo.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        public ActionResult Edit(int? id)
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

        [HttpPost]
        public async Task<IActionResult> Edit(BookInfo book, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    book.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    book.Image = "Images/noimage.PNG";
                }
                _db.BookInfo.Update(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        //GET Details Action Method
        public ActionResult Details(int? id)
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

        //GET Delete Action Method

        public ActionResult Delete(int? id)
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

        //POST Delete Action Method

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _db.BookInfo.FirstOrDefault(c => c.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            _db.BookInfo.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }

}

