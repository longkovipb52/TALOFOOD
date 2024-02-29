using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTW_FFOOD.Models;
using PagedList;
namespace LTW_FFOOD.Controllers
{
    public class ProductController : Controller
    {
        // GET: Sanpham

            public ActionResult Sanphamtheoloai(int? page)
            {
            int pagesize = 8;
            int pageNumber = (page ?? 1);
                using (LTWEB_FFoodEntities db = new LTWEB_FFoodEntities())
                {
                    
                    var categories = (from category in db.Food_Catagory select category).ToList();
                    var foods = new List<Food>();

                    foreach (var category in categories)
                    {
                        foods.AddRange(db.Food.Where(f => f.id_catagory == category.foodcatagory_id).ToList());
                    }

                    var viewModel = new FoodViewModel
                    {
                        Foodss = foods.ToPagedList(pageNumber,pagesize),
                        Categories = categories
                    };

                    return View(viewModel);
                }
            }

        public ActionResult SanphamtheoloaitheoID(int id, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            using (LTWEB_FFoodEntities db = new LTWEB_FFoodEntities())
            {
                var categories = db.Food_Catagory.Find(id);
                var foods = db.Food.Where(f => f.id_catagory == id).ToList();

                var viewModel = new FoodViewModel
                {
                    Foodss = foods.ToPagedList(pageNumber, pageSize),
                    category = categories,
                    Categories = db.Food_Catagory.ToList(),
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItemCount = foods.Count
                };

                return View(viewModel);
            }
        }

        public ActionResult Chitietsanpham(int id)
        {
            using (LTWEB_FFoodEntities db = new LTWEB_FFoodEntities())
            {
                var food = db.Food.Find(id);
                if (food == null)
                {
                    return HttpNotFound();
                }
                var category = db.Food_Catagory.Find(food.id_catagory);

                var foodViewModel = new FoodViewModel
                {
                    food = food,
                    Foods = db.Food.ToList(),
                    Categories = db.Food_Catagory.ToList(),
                    category = category,
                    Quantity = 1
                };

                return View(foodViewModel);
            }
        }
       
            public ActionResult Timkiemsanpham(string search)
            {
                LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
                List<Food> sanphams = new List<Food>();
                
                if (!String.IsNullOrEmpty(search))
                {
                    sanphams = db.Food.Where(s => s.food_name.ToLower().Contains(search.ToLower())).ToList();
                }
            ViewBag.Search = search;
                return View(sanphams);
            }

        }


    }

    