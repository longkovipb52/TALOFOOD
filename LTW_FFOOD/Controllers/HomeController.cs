using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTW_FFOOD.Models;
namespace LTW_FFOOD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home()
        {
            using (LTWEB_FFoodEntities db = new LTWEB_FFoodEntities())
            {
                var categories = (from category in db.Food_Catagory select category).ToList();
                var foods = new List<Food>();
                var contacts = db.Contact.ToList();

                foreach (var category in categories)
                {
                    foods.AddRange(db.Food.Where(f => f.id_catagory == category.foodcatagory_id).Take(6).ToList());
                }

                var viewModel = new FoodViewModel
                {
                    Contacts = contacts,
                    Foods = foods,
                    Categories = categories
                };

                return View(viewModel);
            }
        }
       

    }
}