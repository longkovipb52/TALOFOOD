using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using LTW_FFOOD.Models;
namespace LTW_FFOOD.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        // GET: Admin/LoaiSanPham
        LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
        public ActionResult loaisanpham(int? page, int? pagesize, string search = "")
        {
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 5;
            }
            var catagory = db.Food_Catagory.Where(a => a.foodcategory_name.Contains(search)).ToList();
            return View(catagory.ToPagedList((int)page, (int)pagesize));
        }
        public ActionResult Themmoiloai()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themmoiloai(Food_Catagory catagory)
        {
            db.Food_Catagory.Add(catagory);
            db.SaveChanges();
            return RedirectToAction("loaisanpham");
        }
        public ActionResult Deleteloai(int id)
        {
            Food_Catagory catagory = db.Food_Catagory.Where(r => r.foodcatagory_id == id).FirstOrDefault();
            return View(catagory);
        }
        [HttpPost]
        public ActionResult Deleteloai(Food_Catagory catagory)
        {
            Food_Catagory catagorys = db.Food_Catagory.Where(r => r.foodcatagory_id == catagory.foodcatagory_id).FirstOrDefault();
            db.Food_Catagory.Remove(catagorys);
            db.SaveChanges();
            return RedirectToAction("loaisanpham");
        }
    }
}