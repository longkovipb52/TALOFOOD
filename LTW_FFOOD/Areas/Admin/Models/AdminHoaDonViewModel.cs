using LTW_FFOOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTW_FFOOD.Areas.Admin.Models
{
    public class AdminHoaDonViewModel
    {
        public List<Bill> bills { get; set; }
        public Bill bill { get; set; }
        public List<Bill_info> billinfos { get; set; }
        public Bill_info billinfo { get; set; }
        public List<LTW_FFOOD.Models.Food> foods { get; set; }
        public LTW_FFOOD.Models.Food food { get; set; }
        public LTW_FFOOD.Models.Account account { get; set; }
        public List<Account> accounts { get; set; }
    }
}