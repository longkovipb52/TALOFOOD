using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace LTW_FFOOD.Models
{
    public class FoodViewModel
    {
        public IPagedList<LTW_FFOOD.Models.Food> Foodss { get; set; }

        public List<LTW_FFOOD.Models.Food> Foods { get; set; }
        public List<LTW_FFOOD.Models.Food_Catagory> Categories { get; set; }
        public LTW_FFOOD.Models.Food_Catagory category { get; set; }
        public LTW_FFOOD.Models.Food food { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItemCount / PageSize);
        public List<LTW_FFOOD.Models.Contact> Contacts { get; set; }
        public int Quantity { get; set; }
    }
}