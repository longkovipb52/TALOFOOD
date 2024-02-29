using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTW_FFOOD.Models;
using PayPal.Api;

namespace LTW_FFOOD.Controllers

{
    public class ShoppingCartController : Controller
    {
        // GET: Cart
        LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
        public CartModel Cart()
        {
            CartModel cart = Session["CartModel"] as CartModel;
            if(cart == null|| Session["CartModel"] == null)
            {
                cart = new CartModel();
                Session["CartModel"] = cart;    
            }
            return cart;
        }
        public ActionResult ShowCart()
        {
            if (Session["CartModel"] == null)
            {
                return View(new CartModel()); 
            }
            CartModel cart = Session["CartModel"] as CartModel;
            return View(cart);
        }


        public ActionResult Addtocart(int id, int quantity = 1)
        {
            var food = db.Food.SingleOrDefault(f => f.food_id == id);
            if (food != null)
            {
                Cart().Add(food,quantity);
            }
            return RedirectToAction("ShowCart");
        }
        public ActionResult UpdateCart(FormCollection form)
        {
            CartModel cart = Session["CartModel"] as CartModel;
            int id =int.Parse( form["food_id"]);
            int count = int.Parse(form["_count"]);
            cart.Update(id, count);
            return RedirectToAction("ShowCart");
        }
        public ActionResult DeleteCart(int id)
        {
            CartModel cart = Session["CartModel"] as CartModel;
            cart.Delete(id);
            return RedirectToAction("ShowCart");
        }
       
    }

}
