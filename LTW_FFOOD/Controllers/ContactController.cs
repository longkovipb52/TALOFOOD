using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using LTW_FFOOD.Models;
namespace LTW_FFOOD.Controllers

{
    public class ContactController : DangNhap2Controller
    {
        // GET: Contact
        public ActionResult contact()
        {
            return View();
        }
        public ActionResult themlienhe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult themlienhe(Contact c)
        {
            if (ModelState.IsValid)
            {
                LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
                db.Contact.Add(c);
                db.SaveChanges();
               
                TempData["ThongBaoThanhCong"] = "Gửi thành công";
                return RedirectToAction("contact");
            }
            else
            {
                TempData["ThongBaoLoi"] = "Gửi không thành công";
                return View(c);
            }
        }
    }
}

