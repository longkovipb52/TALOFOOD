using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LTW_FFOOD.Models
{
    public class Cartitem
    {
       
        public Food foods { get; set; }
        public int _count { get; set; }
        
        
    }
    public class CartModel
    {
        List<Cartitem> items = new List<Cartitem>();
        public IEnumerable<Cartitem> Items
        {
            get { return items; }
        }
        public void Add(Food food, int count = 1)
        {
            var item = items.FirstOrDefault(f => f.foods.food_id == food.food_id);
            if(item == null)
            {
                items.Add(new Cartitem
                {
                    foods = food,
                    _count = count
                });
            }
            else
            {
                item._count += count;
            }
        }
        public void Update(int id,int count)
        {
            var item = items.Find(f => f.foods.food_id == id);
            if(item != null)
            {
                item._count = count;
            }
        }
        public void Delete(int id)
        {
            items.RemoveAll(f => f.foods.food_id == id);
        }
    }
}