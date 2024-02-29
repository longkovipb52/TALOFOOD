using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTW_FFOOD.Models;
namespace LTW_FFOOD.Controllers
{
    public class ThongTinKhachHangController : Controller
    {
        // GET: ThongTinKhachHang
        LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
        public ActionResult Thongtin()
        {
            int id_account = (int)Session["UserID"];
            var accounts = db.Account.SingleOrDefault(a => a.account_id == id_account);
            
            
            return View(accounts);
        }
    }
}