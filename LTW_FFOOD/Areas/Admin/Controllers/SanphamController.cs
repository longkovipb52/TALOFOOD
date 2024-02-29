using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTW_FFOOD.Models;
using PagedList;
namespace LTW_FFOOD.Areas.Admin.Controllers
{
    public class SanphamController : Controller
    {
        LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
        // GET: Admin/Sanpham
        public ActionResult sanpham(int? page, int? pagesize, string search = "")
        {
            if (Session["UserID"] == null)
            {
                // Chuyển hướng người dùng đến trang đăng nhập
                return RedirectToAction("Login", "DangNhap");
            }
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 5;
            }
            var food = db.Food.Where(a => a.food_name.Contains(search)).ToList();
            return View(food.ToPagedList((int)page, (int)pagesize));
        }
        public ActionResult Themmoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themmoi(Food food)
        {
            LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
            db.Food.Add(food);
            db.SaveChanges();
            return RedirectToAction("sanpham");
        }
        public ActionResult Edit(int id)
        {
            LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
            Food food = db.Food.Where(r => r.food_id == id).FirstOrDefault();
            return View(food);
        }
        [HttpPost]
        public ActionResult Edit(Food food)
        {
            LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
            Food foods = db.Food.Where(r => r.food_id == food.food_id).FirstOrDefault();
            foods.food_name = food.food_name;
            foods.food_id = food.food_id;
            foods.id_catagory = food.id_catagory;
            foods.price = food.price;
            foods.description = food.description;
            foods.image = food.image;
            db.SaveChanges();
            return RedirectToAction("sanpham");
        }
        public ActionResult Delete(int id)
        {
            LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
            Food food = db.Food.Where(r => r.food_id == id).FirstOrDefault();
            return View(food);
        }
        [HttpPost]
        public ActionResult Delete(Food food)
        {
            LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
            Food foods = db.Food.Where(r => r.food_id == food.food_id).FirstOrDefault();
            db.Food.Remove(foods);
            db.SaveChanges();
            return RedirectToAction("sanpham");
        }
    }
}